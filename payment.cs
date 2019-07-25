using Stripe;

namespace PaymentBotSample
{
    public class Payment
    {
        string publishableApiKey = StripePaymentProviderSettings.GetPublishableApiKey;
        string secretKey = StripePaymentProviderSettings.GetSecretKey;

        private static readonly string SuccessStatus = "succeeded";
        private static readonly string PaymentDescription = "Payment from Bot";
        private static readonly string Currency = "usd";

        public bool ProcessPayment(Card creditCard)
        {          

            StripeConfiguration.ApiKey = publishableApiKey;
            var chargeId = CreateCharge(creditCard);

            var options = new RequestOptions
            {
                ApiKey = secretKey
            };
            var service = new ChargeService();
            Charge charge = service.Get(
              chargeId, null,
              options
            );

            return charge.Status == SuccessStatus ? true : false;
        }

        private string CreateCharge(Card creditCard)
        {
            StripeConfiguration.ApiKey = secretKey;

            var tokenId = CreateToken(creditCard);

            var options = new ChargeCreateOptions
            {
                Amount = (10 * 100),
                Currency = Currency,
                Description = PaymentDescription,
                Source = tokenId //"tok_mastercard" // obtained with Stripe.js,

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            return charge.Id;
        }

        private string CreateToken(Card creditCard)
        {
            StripeConfiguration.ApiKey = secretKey;

            var options = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = creditCard.CreditCardNumber,//"test card - 4242424242424242",
                    ExpYear = creditCard.ExpiryYear, //any future year
                    ExpMonth = creditCard.ExpiryMonth, //any number
                    Cvc = creditCard.CVC //any 3 digit number
                }
            };

            var service = new TokenService();
            Token stripeToken = service.Create(options);

            return stripeToken.Id;
        }
    }
}