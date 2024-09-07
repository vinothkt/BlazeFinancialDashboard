
//Problem #1
//-----------------------------------------------------------------------------------------
class Animal
{
    public virtual string speak(int x) { return "silence"; }
}

class Cat : Animal
{
    public string speak(int x) { return "meow"; }
}

class Dog : Animal
{
    public string speak(short x) { return "bow-wow"; } //error
    public override string speak(int x) { return "bow-wow"; } //correct
}

//----------------------------------------------------------------------------------------------

class Program1
{
    static void Main(string[] args)
    {
        Animal d = new Dog();
        Console.WriteLine(d.speak(0)); //This prints "silence",
                                       //I assume, intention was to override the base class Animal method, but 'speak' method in Dog does not have same type (int) and it does not have 'override' keyword
                                       //instead, method speak(short x) in Dog child class does a method shaddowing instead of overridding; and it creates its own method.
                                       //line#15 (public override string speak(int x)) => adding override keyword and using int datatype, can fix this problem and prints "bow-wow"
    }
}
