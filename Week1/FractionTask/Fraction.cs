namespace Week1.FractionTask
{
    public class Fraction
    {
        private readonly int _numerator;
        private readonly int _denominator;

        public Fraction(int num, int den)
        {
            if (den == 0)
            {
                throw new ArgumentException("Denominator can not be zero", nameof(den));
            }
            _numerator = num;
            _denominator = den;
        }

        public static Fraction operator +(Fraction exp) => exp;
        public static Fraction operator -(Fraction exp) => new Fraction(-exp._numerator, exp._denominator);

        public static Fraction operator +(Fraction firstExp, Fraction secondExp) => new Fraction(firstExp._numerator * secondExp._denominator + secondExp._numerator * firstExp._denominator, firstExp._denominator * secondExp._denominator);

        public static Fraction operator -(Fraction firstExp, Fraction secondExp) => new Fraction(firstExp._numerator * secondExp._denominator - secondExp._numerator * firstExp._denominator, firstExp._denominator * secondExp._denominator);

        public static Fraction operator *(Fraction firstExp, Fraction secondExp) => new Fraction(firstExp._numerator * secondExp._numerator, firstExp._denominator * secondExp._denominator);

        public static Fraction operator /(Fraction firstExp, Fraction secondExp)
        {
            if (secondExp._numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(firstExp._numerator * secondExp._denominator, firstExp._denominator * secondExp._numerator);
        }

        public static double FromFractionToDouble(Fraction fr) => (double)fr._numerator / (double)fr._denominator;

        public override string ToString() => $"{_numerator} / {_denominator}";

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 29 + _numerator.GetHashCode();
            hash = hash * 29 + _denominator.GetHashCode();
            return hash;
        }

        public override bool Equals(object? obj)
        {
            Fraction? fr = obj as Fraction;
            if (fr == null)
                return false;
            return fr._numerator == _numerator && fr._denominator == _denominator;
        }
    }
}
