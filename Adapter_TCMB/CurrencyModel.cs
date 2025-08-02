namespace Adapter_TCMB
{
    /*
     <Currency CrossOrder="0" Kod="USD" CurrencyCode="USD">
			<Unit>1</Unit>
			<Isim>ABD DOLARI</Isim>
			<CurrencyName>US DOLLAR</CurrencyName>
			<ForexBuying>39.1192</ForexBuying>
			<ForexSelling>39.1896</ForexSelling>
			<BanknoteBuying>39.0918</BanknoteBuying>
			<BanknoteSelling>39.2484</BanknoteSelling>
			<CrossRateUSD/>
			<CrossRateOther/>
		
	</Currency>
     */
    public class CurrencyModel
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? CrossRateUSD { get; set; }
        public decimal? CrossRateOther { get; set; }
    }
}
