
class A
{
    public int a { get; set; }
    public int b { get; set; }
}

class B : Animal
{
    //public const A a; //Reference type A cannot be as constant; you can use readonly
    public readonly A aa;

    public B() {
        //a.a = 10; //cannot assign value in constructore for const variable
        aa = new A(); //initialize the instance before assign value
        aa.a = 10; //set value
    } 
}

class Program2
{
    static void Main1(string[] args) //rename back to Main method for testing
    {
        B b = new B();
        //Console.WriteLine("%d %d\n",b.a.a,b.a.b); //this format is invalid
        Console.WriteLine("{0} {1}", b.aa.a, b.aa.b); //valid format - answer would be 10 0
    }
}
