namespace Week2.DelegateTask
{
    public class SimpleCustomerFactory
    {
        private readonly Dictionary<string, Func<Customer, double>> DiscountCalculation;

        public SimpleCustomerFactory(Dictionary<string, Func<Customer, double>> discountCalculation)
        {
            DiscountCalculation = discountCalculation;
        }

        public double CalculateDiscount(Customer customer)
        {
            if (!DiscountCalculation.ContainsKey(customer.CustomerType))
            {
                throw new NotImplementedException("Customer type is not defined");
            }

            return DiscountCalculation[customer.CustomerType].Invoke(customer);
        }
    }
}
