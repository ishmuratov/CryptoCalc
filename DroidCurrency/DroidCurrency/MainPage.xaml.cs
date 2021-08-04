using DroidCurrency.Helpers;
using DroidCurrency.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DroidCurrency
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DataBase db = (DataBase)FileWorker.LoadDataFromFile(Path.Combine(App.FolderPath, "user.dat"));
        List<Coin> Coins = new List<Coin>();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCryptocoins();
            LoadCoinsInPickers();
            DisplayCoinsOnGrid();
            UpdateCryptoDataFromInternet();
            UpdateCurrencyDataFromInternet();
        }

        private void LoadCryptocoins()
        {
            if (db == null)
                db = new DataBase();
            Coins = db.Data;
            if (Coins != null && Coins.Count != 0)
                return;
            Coin USD = new Coin { Name = "Dollar", CharCode = "USD", LocalCurrencyValue = 1, IconFileName = "usd.jpg", PaperMoney = true };
            Coins.Add(USD);
            Coin EUR = new Coin { Name = "EURO", CharCode = "EUR", LocalCurrencyValue = 0.0116M, IconFileName = "eur.jpg", PaperMoney = true };
            Coins.Add(EUR);
            Coin RUB = new Coin { Name = "Ruble", CharCode = "RUB", LocalCurrencyValue = 0.0137M, IconFileName = "rus.jpg", PaperMoney = true };
            Coins.Add(RUB);
            Coin BYN = new Coin { Name = "Belarus Ruble", CharCode = "BYN", LocalCurrencyValue = 0.411M, IconFileName = "byn.jpg", PaperMoney = true };
            Coins.Add(BYN);
            Coin BTC = new Coin { Name = "Bitcoin", CharCode = "BTC", IconFileName = "bitcoin.png" };
            Coins.Add(BTC);
            Coin ETH = new Coin { Name = "Ethereum", CharCode = "ETH", IconFileName = "ethereum.png" };
            Coins.Add(ETH);
            Coin XMR = new Coin { Name = "Monero", CharCode = "XMR", IconFileName = "monero.png" };
            Coins.Add(XMR);
            Coin LTC = new Coin { Name = "Litecoin", CharCode = "LTC", IconFileName = "litecoin.png" };
            Coins.Add(LTC);
            Coin XRP = new Coin { Name = "XRP", CharCode = "XRP", IconFileName = "ripple.png" };
            Coins.Add(XRP);
        }

        private void LoadCoinsInPickers()
        {
            pickerCurrency1.Title = "BTC";
            imageCurrency1.Source = "bitcoin.png";
            pickerCurrency2.Title = "USD";
            imageCurrency2.Source = "usd.jpg";

            foreach (Coin anyCoin in Coins)
            {
                pickerCurrency1.Items.Add(anyCoin.CharCode);
                pickerCurrency2.Items.Add(anyCoin.CharCode);
            }
        }

        private async void UpdateCryptoDataFromInternet()
        {
            lbStatus.Text = "Загрузка курсов...";
            lbStatus.TextColor = Color.Red;
            await Task.Run(() => GetCoinsValues());
            DisplayCoinsOnGrid();
            lbStatus.Text = "";
            SaveData();
        }

        private void GetCoinsValues()
        {
            foreach (Coin anyCoin in Coins)
            {
                if (anyCoin.CharCode == "USD")
                {
                    anyCoin.LocalCurrencyValue = 1;
                    continue;
                }
                if (anyCoin.PaperMoney)
                {
                    continue;
                }

                string stringUSDValue = CryptoCoins.GetPrice(anyCoin.Name.ToLower()).Replace("$", "").Replace(",", "");
                if (stringUSDValue.Length > 10)
                    DisplayAlert("Ошибка!", stringUSDValue, "OK");
                decimal USDValue = DecimalHelper.StringToDecimal(stringUSDValue);
                SetCoinProperties(anyCoin, USDValue);
                anyCoin.LocalCurrencyValue = USDValue;
            }
        }

        private void SetCoinProperties(Coin coin, decimal USDPrice)
        {
            if (coin.LocalCurrencyValue < USDPrice)
                coin.Increased = true;
            else
                coin.Increased = false;
            if (coin.LocalCurrencyValue == USDPrice)
                coin.Stable = true;
            else
                coin.Stable = false;
        }

        private void SaveData()
        {
            FileWorker.SaveDataToFile(db, Path.Combine(App.FolderPath, "user.dat"));
        }

        private async void UpdateCurrencyDataFromInternet()
        {
            await Task.Run(() => GetCurrencyValues());
        }

        private void GetCurrencyValues()
        {
            string USDinRUB = Currencies.GetPrice("USD");
            decimal RUBinUSD = 1 / DecimalHelper.StringToDecimal(USDinRUB);
            foreach (Coin anyCoin in Coins)
            {
                if (anyCoin.CharCode == "RUB")
                {
                    anyCoin.LocalCurrencyValue = RUBinUSD;
                }
                if (anyCoin.CharCode == "BYN")
                {
                    anyCoin.LocalCurrencyValue = DecimalHelper.StringToDecimal(Currencies.GetPrice("BYN")) * RUBinUSD;
                }
                if (anyCoin.CharCode == "EUR")
                {
                    anyCoin.LocalCurrencyValue = DecimalHelper.StringToDecimal(Currencies.GetPrice("EUR")) * RUBinUSD;
                }
            }
        }

        private void DisplayCoinsOnGrid()
        {
            foreach (Coin anyCoin in Coins)
            {
                if (anyCoin.CharCode == "BTC")
                {
                    ChangeColor(lbBitcoin, anyCoin);
                    lbBitcoin.Text = $"$ {anyCoin.LocalCurrencyValue}";
                    imgBTC.Source = anyCoin.IconFileName;
                }
                if (anyCoin.CharCode == "ETH")
                {
                    ChangeColor(lbEthereum, anyCoin);
                    lbEthereum.Text = $"$ {anyCoin.LocalCurrencyValue}";
                    imgETH.Source = anyCoin.IconFileName;
                }
                if (anyCoin.CharCode == "LTC")
                {
                    ChangeColor(lbLiteCoin, anyCoin);
                    lbLiteCoin.Text = $"$ {anyCoin.LocalCurrencyValue}";
                    imgLTC.Source = anyCoin.IconFileName;
                }
                if (anyCoin.CharCode == "XMR")
                {
                    ChangeColor(lbMonero, anyCoin);
                    lbMonero.Text = $"$ {anyCoin.LocalCurrencyValue}";
                    imgXMR.Source = anyCoin.IconFileName;
                }
                if (anyCoin.CharCode == "XRP")
                {
                    ChangeColor(lbRipple, anyCoin);
                    lbRipple.Text = $"$ {anyCoin.LocalCurrencyValue}";
                    imgXRP.Source = anyCoin.IconFileName;
                }
            }
        }

        private void ChangeColor(Label label, Coin coin)
        {
            if (coin.Increased)
                label.TextColor = Color.Green;
            else
                label.TextColor = Color.Red;
            if (coin.Stable)
                label.TextColor = Color.Black;
        }

        private void OnConvertClicked(object sender, EventArgs e)
        {
            decimal result = 0;
            decimal CurrencyForConvertAmount = 0;
            string charCodeFrom = pickerCurrency1.SelectedItem == null ? pickerCurrency1.Title : pickerCurrency1.SelectedItem.ToString();
            string charCodeTo = pickerCurrency2.SelectedItem == null ? pickerCurrency2.Title : pickerCurrency2.SelectedItem.ToString();
            if (numCurencyAmount1.Text == "")
            {
                return;
            }

            foreach (Coin anyCoin in Coins)
            {
                if (charCodeFrom == anyCoin.CharCode)
                {
                    try
                    {
                        CurrencyForConvertAmount = Convert.ToDecimal(numCurencyAmount1.Text) * anyCoin.LocalCurrencyValue;
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Ошибка!", ex.Message , "OK");
                    }
                }
            }

            foreach (Coin anyCoin in Coins)
            {
                if (charCodeTo == anyCoin.CharCode)
                {
                    result = CurrencyForConvertAmount / anyCoin.LocalCurrencyValue;
                }
            }

            numCurrencyAmount2.Text = result.ToString("0.000");
        }

        private void OnUpdateClicked(object sender, EventArgs e)
        {
            UpdateCryptoDataFromInternet();
        }

        private void OnPicker1Changed(object sender, EventArgs e)
        {
            Picker anyPicker = sender as Picker;
            string pickerCharCode = anyPicker.SelectedItem == null ? anyPicker.Title : anyPicker.SelectedItem.ToString();
            foreach (Coin anyCoin in Coins)
            {
                if (anyCoin.CharCode == pickerCharCode)
                {
                    if (anyPicker.Id == pickerCurrency1.Id)
                        imageCurrency1.Source = anyCoin.IconFileName;
                    else
                        imageCurrency2.Source = anyCoin.IconFileName;
                }
            }
        }
    }
}
