if (args.Length != 1)
{
    System.Console.WriteLine("wrong number of arguments");
    return;
}
if (args[0].Length != 15)
{
    System.Console.WriteLine("invalid length of arguments");
    return;
}
if (args[0][3] != '-' || args[0][7] != '-' || args[0][11] != '-')
{
    System.Console.WriteLine("missing ");
    return;
}
if (TryDecode4ColorBands(args[0].Substring(0, 3), args[0].Substring(4, 3), args[0].Substring(8, 3), args[0].Substring(12, 3), args[0].Substring(16, 3),out double resistorValue, out double tolerance))
{
    Decode4ColorBands(args[0], out resistorValue, out tolerance);
    System.Console.WriteLine($"resistorValue={resistorValue}; tolerance={tolerance} ");
}

int ConvertColorToDigit(string input)
{
    double tolerance = 0;
    double resistor = 0;

    Decode4ColorBands(input, out resistor, out tolerance);

    System.Console.WriteLine($"resistor={resistor}, tolerance={tolerance}");

    switch (input)
    {
        case "Bla":
            return 0;

        case "Bro":
            return 1;

        case "Red":
            return 2;

        case "Ore":
            return 3;

        case "Yel":
            return 4;

        case "Gre":
            return 5;

        case "Blu":
            return 6;

        case "Vio":
            return 7;

        case "Gra":
            return 8;

        case "Whi":
            return 9;
    }
    return -1;
}

double GetMultiplierFromColor(string multiplier)
{
    switch (multiplier)
    {
        case "Bla":
            return 1;

        case "Bro":
            return 10;

        case "Red":
            return 100;

        case "Ore":
            return 1000;

        case "Yel":
            return 10000;

        case "Gre":
            return 100000;

        case "Blu":
            return 1000000;

        case "Vio":
            return 10000000;

        case "Gra":
            return 100000000;

        case "Whi":
            return 1000000000;

        case "Gol":
            return 0.1;

        case "Sil":
            return 0.01;
    }
    return -1;
}

double GetToleranceFromColor(string tolerance)
{
    switch (tolerance)
    {
        case "Bla":
            return 0;

        case "Bro":
            return 1;

        case "Red":
            return 2;

        case "Ore":
            return 0;

        case "Yel":
            return 0;

        case "Gre":
            return 0.5;

        case "Blu":
            return 0.25;

        case "Vio":
            return 0.10;

        case "Gra":
            return 0.05;

        case "Whi":
            return 0;

        case "Gol":
            return 5;

        case "Sil":
            return 10;
    }
    return -1;
}

void Decode4ColorBands(string input, out double resistor, out double tolerance)
{
    string result = string.Empty;

    result += ConvertColorToDigit(input.Substring(0, 3));
    input = input.Substring(4);

    result += ConvertColorToDigit(input.Substring(0, 3));
    input = input.Substring(4);

    result += ConvertColorToDigit(input.Substring(0, 3));
    input = input.Substring(4);

    resistor = double.Parse(result) * GetMultiplierFromColor(input.Substring(0, 3));
    input = input.Substring(0, 3);

    tolerance = GetToleranceFromColor(input);
}

bool TryConvertColorToDigit(string color, out int digit)
{
    switch (color)
    {
        case "Bla": digit = 0; break;
        case "Bro": digit = 1; break;
        case "Red": digit = 2; break;
        case "Ora": digit = 3; break;
        case "Yel": digit = 4; break;
        case "Gre": digit = 5; break;
        case "Blu": digit = 6; break;
        case "Vio": digit = 7; break;
        case "Gra": digit = 8; break;
        case "Whi": digit = 9; break;
        default:
            digit = -1;
            return false;
    }
    return true;
}

bool TryGetMultiplierFromColor(string color, out double multiplier)
{
    switch (color)
    {
        case "Gol": multiplier = 0.1d; break;
        case "Sil": multiplier = 0.01d; break;
        default:
            int digit;
            if (TryConvertColorToDigit(color, out digit))
            {
                multiplier = -1d;
                return false;
            }
            multiplier = Math.Pow(10, digit);
            break;
    }
    return true;
}

bool TryGetToleranceFromColor(string toleranceColor, out double tolerance)
{
    switch (toleranceColor)
    {
        case "Bro":
            tolerance = 1;
            break;

        case "Red":
            tolerance = 2;
            break;

        case "Gre":
            tolerance = 0.5;
            break;

        case "Blu":
            tolerance = 0.25;
            break;

        case "Vio":
            tolerance = 0.1;
            break;

        case "Gra":
            tolerance = 0.05;
            break;

        case "Gol":
            tolerance = 5;
            break;

        case "Sil":
            tolerance = 10;
            break;

        default:
            tolerance = -1;
            return false;
    }
    return true;
}

bool TryDecode4ColorBands(string color1, string color2, string color3, string color4,string color5, out double resistorValue, out double tolerance)
{
    resistorValue = -1;
    tolerance = -1;

    if (!TryConvertColorToDigit(color1, out int digit1)) { return false; }
    if (!TryConvertColorToDigit(color2, out int digit2)) { return false; }
    if (!TryConvertColorToDigit(color5, out int digit5)) { return false; }
    if (!TryGetMultiplierFromColor(color3, out double multiplier)) { return false; }
    if (!TryGetToleranceFromColor(color4, out tolerance)) { return false; }

    resistorValue = (digit1 * 10 + digit2) * multiplier;
    return true;
}