/*
 *
 * Includes
 *
 */
#include "Classes.h"
#include <iostream>

/*
 *
 * Namespaces
 *
 */
using namespace std;
using namespace WMM;

int main()
{

	// create classes
	cout << endl << "Creating A" << endl;
	A* classA = new A();
	cout << endl << "Creating B" << endl;
	B* classB = new B();
	A* bAsA = classB;
	
	// print output
	cout << endl << "A" << endl << "--" << endl;
	cout << classA->GetNumberConcrete() << endl;
	cout << classA->GetNumberVirtual() << endl;
	cout << endl << "B" << endl << "--" << endl;
	cout << classB->GetNumberConcrete() << endl;
	cout << classB->GetNumberVirtual() << endl;
	cout << endl << "A as B" << endl << "-------" << endl;
	cout << bAsA->GetNumberConcrete() << endl;
	cout << bAsA->GetNumberVirtual() << endl;
	
	// destroy
	cout << endl << "Deleting A" << endl;
	delete classA;
	cout << endl << "Deleting B" << endl;
	delete classB;
	
    return 0;
}