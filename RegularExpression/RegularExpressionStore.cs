using System.Text.RegularExpressions;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {
        private static readonly Regex _emailRegex = new Regex(@"\s*[a-z]+.[a-z]+@[a-z]+.com\s*$");
        private static readonly Regex _nameJsonRegex = new Regex("(?<=\")\\w+(?=\":)");
        private static readonly Regex _valueJsonRegex = new Regex(@"((?<=:)|(?<=:""))\w+(?=""?)");
        private static readonly Regex _nameXmlRegex = new Regex(@"(?<=<)\w+(?=>| \w+:\w+=""\w+"")");
        private static readonly Regex _valueXmlRegex = new Regex(@"(?<=>)\w+(?=<)");
        private static readonly Regex _phoneRegex = new Regex(@"(?<!\d)((((\+?38)|0(68|95))[\.\d\s-]{7,16})|\(?067[\)\-\.]+[\.\d\s-]{7,16})(?!\d)");
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {
            return _emailRegex.IsMatch(input);
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {
            var namesCollection = _nameJsonRegex.Matches(inputJson);
            return namesCollection.Select(element => element.Value).ToArray();
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {
            var namesCollection = _valueJsonRegex.Matches(inputJson);
            return namesCollection.Select(element => element.Value).ToArray();
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            var namesCollection = _nameXmlRegex.Matches(inputXml);
            return namesCollection.Select(element => element.Value).ToArray();
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            var namesCollection = _valueXmlRegex.Matches(inputXml);
            return namesCollection.Select(element => element.Value).ToArray();
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {
            var phonesMatches = _phoneRegex.Matches(input);
            
            return phonesMatches.Select(el => el.Value.StartsWith("38")?  "+" + el.Value : el.Value).ToArray();
        }
    }
}