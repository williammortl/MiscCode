#ifndef CLASSES_H
#define CLASSES_H

/*
 *
 * Namespace
 *
 */
namespace WMM
{
	class A;
	class B;
}

/*
 *
 * Class definitions
 *
 */

// base / super class
class WMM::A
{
	private:
		int nNumberVirtual;
	protected:
		int nNumberConcrete;
	public:
		int GetNumberConcrete(void);
		virtual int GetNumberVirtual(void);
		A();
		~A();
};

// sub / derived class
class WMM::B: public WMM::A
{
	private:
		typedef WMM::A base;
		int nNumberVirtual;
	public:
		int GetNumberConcrete(void);
		virtual int GetNumberVirtual(void);
		B();
		~B();
};

#endif