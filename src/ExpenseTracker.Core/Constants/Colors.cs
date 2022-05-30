using System.Collections.Generic;

namespace ExpenseTracker.Core.Constants
{
    public static class Colors
    {
        public static List<Color> GetColors = new()
        {
            new Color("Dark",
                "#232E3C"),
            new Color("Blue",
                "#206BC4"),
            new Color("Azure",
                "#4299E1"),
            new Color("Indigo",
                "#4263EB"),
            new Color("Purple",
                "#AE3EC9"),
            new Color("Pink",
                "#D6336C"),
            new Color("Red",
                "#D63939"),
            new Color("Orange",
                "#F76707"),
            new Color("Yellow",
                "#F59F00"),
            new Color("Lime",
                "#74b816")
        };

        public record Color(string name, string hexValue)
        {
            public string name { get; init; } = name;
            public string hexValue { get; init; } = hexValue;
        }
    }
}