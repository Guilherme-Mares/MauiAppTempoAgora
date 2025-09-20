using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var networkAccess = Connectivity.Current.NetworkAccess;

            if (networkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Erro de Conexão", "Você está sem conexão com a internet. Verifique sua rede e tente novamente.", "OK");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} .\n" +
                            $"Longitude: {t.lon} .\n" +
                            $"Nascer do sol: {t.sunrise} .\n" +
                            $"Por do Sol: {t.sunset} .\n" +
                            $"Temp Máx: {t.temp_max} .\n" +
                            $"Descrição: {t.description} .\n" +
                            $"Velocidade do vento: {t.speed} .\n" +
                            $"Visibilidade: {t.visibility} metros .\n" +
                            $"Temp Min: {t.temp_min} .\n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {

                        lbl_res.Text = "Sem dados de previsão.";


                    }


                }
                else 
                {

                    lbl_res.Text = "Preencha a cidade.";
                
                }

            } catch (Exception ex)
            
            {
                await DisplayAlert("Ops", ex.Message, "OK");

            }
        
        }
    }

}
