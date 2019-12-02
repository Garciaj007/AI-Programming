#include "stdafx.h"

#include "FFLLAPI.h"	// FFLL API
#include <iostream>	// for i/o functions

#define NUM_ENEMY		0 // number of enemy is 1st variable
#define DISTANCE_ENEMY	1 // distance is 2nd variable

#pragma comment(lib, "ffllapi.lib")

using namespace std;

int main(int argc, char* argv[])
{
	float	num_enemy, distance_enemy; // values for input variables
	char	option;	// var for selection of what user wants to do

	// create a model
	int model = ffll_new_model();

	int ret_val = (int)ffll_load_fcl_file(model, "lab8.fcl");

	if (ret_val < 0)
	{
		cout << "Error in the script" << endl;
		cout << ffll_get_msg_textA(model) << endl;

		return 0;
	}

	// create a child for the model...
	int child = ffll_new_child(model);

	while (1)
	{
		cout << "SELECT AN OPTION:\n\tS - set values\n\tQ - quit";
		cout << endl;
		cin >> option;

		if (option == 'Q' || option == 'q')
			break;

		if (option == 'S' || option == 's')
		{
			cout << "Number of enemy: ";
			cin >> num_enemy;
			cout << "Distance to enemy: ";
			cin >> distance_enemy;
			cout << "Aggressiveness: ";

			// To do : set input variables (num_enemy and distance_enemy) using the ffll_set_value function.
			ffll_set_value(model, child, NUM_ENEMY, num_enemy);
			ffll_set_value(model, child, DISTANCE_ENEMY, distance_enemy);

			// To do : get the output value using the ffll_get_output_value function.
			double output = ffll_get_output_value(model, child);

			// To do : uncomment the following after completing the above part
			cout << "Flame strength = " << output;

			cout << endl;
		}
	}

	return 0;
}


