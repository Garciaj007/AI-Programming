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

int AStar::SearchThread(void * data)
{
	AStar* astar = static_cast<AStar*>(data);

	if (!astar->startNode || !astar->goalNode)
	{
		astar->OnSearchDone();
		return 0;
	}

	// To do: Complete this function.






	astar->OnSearchDone();
	return 0;
}

Node * AStar::GetClosestFromUnvisited()
{

	float shortest = std::numeric_limits<float>::max();
	Node* shortestNode = nullptr;

	// To do: Complete this function.





	return shortestNode;
}

void AStar::ValidateDistanceDict(Node * n)
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
