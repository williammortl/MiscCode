#ifndef CLASSES_H
#define CLASSES_H



/*
 *
 * Class definitions
 *
 */
namespace WMM
{

	// base / super class
	class A
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
	class B: public A
	{
		private:
			typedef A base;
			int nNumberVirtual;
		public:
			int GetNumberConcrete(void);
			virtual int GetNumberVirtual(void);
			B();
			~B();
	};
}


#endif