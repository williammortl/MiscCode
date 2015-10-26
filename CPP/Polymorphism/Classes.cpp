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

/*
 *
 * Class method definitions
 *
 */
 
// base / super class
int A::GetNumberConcrete(void)
{
	return this->nNumberConcrete;
}

int A::GetNumberVirtual(void)
{
	return this->nNumberVirtual;
}

A::A()
{
	this->nNumberConcrete = 1;
	this->nNumberVirtual = 2;
	cout << "A constructor" << endl;
}

A::~A()
{
	cout << "A destructor" << endl;
}

// sub / derived class
int B::GetNumberConcrete(void)
{
	return this->nNumberConcrete + this->nNumberConcrete;
}

int B::GetNumberVirtual(void)
{
	return base::GetNumberVirtual() + nNumberVirtual;
}

B::B() : A()
{
	this->nNumberVirtual = 3;
	cout << "B constructor" << endl;
}

B::~B()
{
	cout << "B destructor" << endl;
}
