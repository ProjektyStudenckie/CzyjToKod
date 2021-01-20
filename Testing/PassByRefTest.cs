class PassByRefTest 
{

    public static void ChangeMe(out string s) 
    {
        s = "Changed";
    }

    public static void Swap(ref int x, ref int y) 
    {
        int temp = x;

        x = y;
        y = temp;
    }

    public static void Main(string[] args) 
    {
        int a = 5;
        int b = 10;
        string s = "still unchanged";

        Swap(ref a, ref b);
        ChangeMe(out s);

        System.Console.WriteLine("a = " + a + ", " +
                                 "b = " + b + ", " +
                                 "s = " + s);
    }
}