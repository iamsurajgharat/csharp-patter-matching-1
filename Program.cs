using System;

namespace csharp_patter_matching_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoIsOperatorWithPatternMatching();
            DemoSwitchStatementWithPatternMatching();
            DemoSwitchExpressionWithPatternMatching();
        }

        private static void DemoIsOperatorWithPatternMatching()
        {
            int int1 = 10;
            object obj1 = new object();
            object obj2Int = int1;
            object obj3Null = null;

            // #1 "is" operator against reference type String
            bool res1 = obj1 is String;         // false

            // #1 "is" operator against value type struct Int32
            bool res2 = obj2Int is int;         // true

            // Please note that though obj3Null contains null value, it does not match against String
            bool res3 = obj3Null is String;     // false

            // Null values need to handle explicitly by matching against null itself
            bool res4 = obj3Null is null;       // true

            // extracting matched data if the typeis matched. The type of value variable is int
            int res5 = 0;
            if (obj2Int is Int32 value)
            {
                res5 = value + 100;         // 110
            }

            Console.WriteLine(GetJustifiedText("Results from DemoIsOperatorWithPatternMatching starting"));
            Console.WriteLine(res1);
            Console.WriteLine(res2);
            Console.WriteLine(res3);
            Console.WriteLine(res4);
            Console.WriteLine(res5);
            Console.WriteLine(GetJustifiedText("Results from DemoIsOperatorWithPatternMatching ended"));
        }

        private static void DemoSwitchStatementWithPatternMatching()
        {
            object obj1 = "abcdefgh";
            /*object obj2 = "Just a string";
            object obj3 = null;
            String stringNull = null;
            object obj4Null = stringNull;
            object obj5Int = 100;*/


            String result = "";
            switch (obj1)
            {
                // Matches if and only if input is of type String and it also start with "abc", such as obj1 in this example
                case String s when s.StartsWith("abc"):
                    result = "case with String type and when clause";
                    break;

                // Matches if and only if input is of type String (except the input should not match into above case and null strings)
                case String s:
                    result = "case with String type and no when clause";
                    break;

                // Matches if and only if input is null value. Please note obj4Null is match for this case instead of above String case.
                case null:
                    result = "case null";
                    break;

                // This is like catch all case, it matches against eveything including null, and captures in variable value.
                case var value:
                    result = "This is var case : " + value;
                    break;
            }

            Console.WriteLine(result);
        }

        private static void DemoSwitchExpressionWithPatternMatching()
        {
            Console.WriteLine(GetJustifiedText("DemoSwitchExpressionWithPatternMatching starts"));
            object obj1 = 1000;
            object obj2 = new SuperHero { Name = "Bruce Wayne" };
            object obj3 = new SuperHero { Name = "Steve Rogers" };
            object obj4 = null;
            object obj5 = new UnknownClass();
            var result1 = obj1 switch
            {
                // Match on if it is of type String and also starts with "abc
                String s when s.StartsWith("abc") => "Its a string that starts with abc",

                // Match if it is of type String
                String s => "Its a string",

                // This is called Property based pattern
                // Match if it is of type SuperHero and also its Name property should have value "Bruce Wayne" and CabFly property true
                SuperHero { Name: "Bruce Wayne", CanFly: true } => "Its batman that can fly",

                // Match if it is of type SuperHero and also its Name property should have value "Bruce Wayne", however CabFly property can be anything.
                SuperHero { Name: "Bruce Wayne" } => "Its batman",

                // This is called Positional pattern. It makes use of Deconstruct method internally

                // Match if it is of type SumperHero and capture its name and canFly values in provided variables using its deconstruct method
                SuperHero(var name, var fly) => $"Unknown superhero, {name} who can " + (fly ? "fly" : "not fly"),

                // Matches null
                null => "Null value",

                // Match anything missed so far
                _ => "Dont know what it is!"

            };

            Console.WriteLine("Result1 :" + result1);

            String importance = "Important";
            String urgency = "Not Urgent";

            // This is called Tuple pattern
            var result2 = (importance, urgency) switch
            {
                ("Important", "Urgent") => "Do it now",
                ("Not Important", "Urgent") => "Delegate",
                ("Important", "Not Urgent") => "Plan it",
                ("Not Important", "Not Urgent") => "Skip it",
                (_, _) => "Not sure what to do!"
            };

            Console.WriteLine("Result2 :" + result2);

            Console.WriteLine(GetJustifiedText("DemoSwitchExpressionWithPatternMatching ends"));
        }

        private static String GetJustifiedText(String text)
        {
            int textLength = text.Length;
            int totalWidth = Console.WindowWidth;
            int remaining = totalWidth - textLength;
            int remainingHalf = remaining / 2;
            String leftPaddedText = text.PadLeft(remainingHalf + textLength, '-');
            return leftPaddedText.PadRight(totalWidth, '-');
        }

    }

    // class SuperHero with Deconstruct method
    public class SuperHero
    {
        public String Name { get; set; }
        public bool CanFly { get; set; }

        public void Deconstruct(out String name, out bool canFly)
        {
            (name, canFly) = (Name, CanFly);
        }
    }

    public class UnknownClass
    {
        public int UnknownProperty { get; set; }
    }
}