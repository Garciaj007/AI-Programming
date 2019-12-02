#include "AStar.h"
#include "Map.h"
#include "SDL.h"

AStar::AStar(Map* m)
	:map(m),
	isSearching(false)
{
	graph = m->GetGraph();
}

AStar::~AStar()
{
}

bool AStar::IsSearching()
{
	return isSearching;
}

void AStar::Search(Node* start, Node* goal)
{
	isSearching = true;
	startNode = start;
	goalNode = goal;

	thread = SDL_CreateThread(SearchThread, "", this);
}

void AStar::OnSearchDone()
{
	isSearching = false;

	// Draw the shortest path
	for (auto p : pathFound)
	{
		map->SetPathMap(p->position, Map::RESULT_PATH_FOUND); // the second param value '2' means that it will draw
	}
}

int AStar::SearchThread(void* data)
{
	AStar* astar = static_cast<AStar*>(data);

	if (!astar->startNode || !astar->goalNode)
	{
		astar->OnSearchDone();
		return 0;
	}

	// To do: Complete this function.
	for (auto& node : astar->graph->GetAllNodes())
	{
		astar->ValidateDistanceDict(&node);
		astar->unvisited.emplace_back(&node);
	}

	astar->distanceDict[astar->startNode] = 0;
	astar->actualDistanceDict[astar->startNode] = 0;

	astar->visited.clear();
	astar->predecessorDict.clear();

	while (!astar->unvisited.empty())
	{
		SDL_Delay(500.f);
		
		auto u = astar->GetClosestFromUnvisited();

		if (u == astar->goalNode) break;

		astar->map->SetPathMap(u->position, Map::SEARCH_IN_PROGRESS);

		astar->visited.push_back(u);

		for (auto adj : astar->graph->GetAdjacentNodes(u))
		{
			if (std::find(astar->visited.begin(), astar->visited.end(), adj) != astar->visited.end())
				continue;

			auto trueDistance = astar->actualDistanceDict[u] + astar->graph->GetDistance(u, adj) + astar->graph->GetDistance(adj, astar->goalNode);
			if (astar->distanceDict[adj] > trueDistance)
			{
				astar->distanceDict[adj] = trueDistance;
				astar->actualDistanceDict[adj] = astar->actualDistanceDict[u] + astar->graph->GetDistance(u, adj);
				astar->predecessorDict.emplace(adj, u);
			}
		}
	}

	astar->OnSearchDone();
	return 0;
}

Node* AStar::GetClosestFromUnvisited()
{
	float shortest = std::numeric_limits<float>::max();
	Node* shortestNode = nullptr;

	// To do: Complete this function.
	for (auto node : unvisited)
	{
		if (shortest > distanceDict[node])
		{
			shortest = distanceDict[node];
			shortestNode = node;
		}
	}

	unvisited.erase(std::find(unvisited.begin(), unvisited.end(), shortestNode));
	return shortestNode;
}

void AStar::ValidateDistanceDict(Node* n)
{
	float max = std::numeric_limits<float>::max();
	if (distanceDict.find(n) == distanceDict.end())
	{
		distanceDict[n] = max;
	}
	if (actualDistanceDict.find(n) == actualDistanceDict.end())
	{
		actualDistanceDict[n] = max;
	}
}
