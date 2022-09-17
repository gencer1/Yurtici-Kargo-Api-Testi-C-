using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YurticiKargoApiTest {
    public partial class Form1:Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Yurtici.createNgiShipmentWithAddress data = new Yurtici.createNgiShipmentWithAddress();

            data.wsUserName = "kullanıcıAdınızıBurayaYazın"; //Api kullanıcı adı
            data.wsPassword = "şifreniziBurayaYazın"; // Api kullanıcı şifresi
            data.wsUserLanguage = "TR"; // Api dili

            Yurtici.XShipmentData shipmentData = new Yurtici.XShipmentData();
            shipmentData.ngiDocumentKey = "GG111222333"; // İrsaliye referans kodu
            shipmentData.cargoType = "2"; // Kargo tipi 2-> koli
            shipmentData.totalCargoCount = 1; // İrsaliyeye ait ürün sayısı
            shipmentData.totalDesi = 16; // İrsaliyeye ait ürün kolilerinin x,y,z çarpımı
            shipmentData.totalWeight = 50; // İrsaliye ürünlerini toplam ağırlığı
            shipmentData.personGiver = "Gencer Ger"; // Satıcı ismi
            shipmentData.description = "Api test gönderisi"; // İrsaliye açıklaması
            shipmentData.productCode = "STA"; // Ürün kodu max 3karakter

            Yurtici.XDocCargoData cargoData = new Yurtici.XDocCargoData();
            cargoData.ngiCargoKey = "GG111222333"; // Kargo referans numarası
            cargoData.cargoType = "2"; //Kargo tipi 2-> koli
            cargoData.cargoDesi = 16;// İrsaliyeye ait ürün kolilerinin x,y,z çarpımı
            cargoData.cargoWeight = 50;// İrsaliye ürünlerini toplam ağırlığı
            cargoData.cargoCount = 1;// İrsaliyeye ait ürün kolilerinin x,y,z çarpımı

            shipmentData.docCargoDataArray = new Yurtici.XDocCargoData[] { cargoData };


            Yurtici.XSpecialFieldData specialData = new Yurtici.XSpecialFieldData();
            specialData.specialFieldName = "54"; // Özel alan kodu 54->irsaliye referans kodu
            specialData.specialFieldValue = "TCSVK000238076"; // Özel alanın değeri

            shipmentData.specialFieldDataArray = new Yurtici.XSpecialFieldData[] { specialData };

            Yurtici.XCodData codData = new Yurtici.XCodData();
            codData.ttInvoiceAmount = 0; // Kargo ücreti
            codData.ttDocumentId = "111222333"; // Fatura no
            codData.ttCollectionType = "0"; 
            codData.ttDocumentSaveType = "1";

            shipmentData.codData = codData;

            data.shipmentData = shipmentData;


            Yurtici.XSenderCustAddress senderData = new Yurtici.XSenderCustAddress();
            senderData.senderCustName = "Gencer Ger"; //Gönderici ismi
            senderData.senderAddress = "Muğla Menteşe ..."; //Gönderici adresi
            senderData.cityId = "48"; // İl plaka kodu
            senderData.townName = "Menteşe"; //İlçe ismi
            senderData.senderMobilePhone = "0541872...."; // gönderici numarası
            senderData.senderEmailAddress = "gen-cer@hotmail.com"; //Gönderici mail adresi        
            data.XSenderCustAddress = senderData; 


            Yurtici.XConsigneeCustAddress consigneeData = new Yurtici.XConsigneeCustAddress();
            consigneeData.consigneeCustName = "Gencer Ger"; // Alıcı adı
            consigneeData.consigneeAddress = "Muğla Menteşe ..."; //Alıcı adresi
            consigneeData.cityId = "48"; //Alıcının il plaka kodu
            consigneeData.townName = "Menteşe"; //Alıcının ilçesi
            consigneeData.consigneeMobilePhone = "0541..."; //Alıcının telefonu
            consigneeData.consigneeEmailAddress = "gen-cer@hotmail.com"; //Alıcının mail adresi     
            

            data.XConsigneeCustAddress = consigneeData;


            Yurtici.XPayerCustData payerData = new Yurtici.XPayerCustData();

            //Yurtiçikargo sisteminde kayıtlı ödeyecek Müşteri kodudur.
            payerData.invCustId = "123321"; //(Aracı firma olduğumuz için ben kendi müşteri kodumu yazdım buraya)

            data.payerCustData = payerData;

            Yurtici.NgiShipmentInterfaceServicesClient client = new Yurtici.NgiShipmentInterfaceServicesClient();


            var result = client.createNgiShipmentWithAddress(data); //Sorguyu gönderir

            textBox1.Text = result.XShipmentDataResponse.outResult;
        }

        private void button2_Click(object sender, EventArgs e) {
            YurticiInfo.WsReportWithReferenceServicesClient client = new YurticiInfo.WsReportWithReferenceServicesClient();


            YurticiInfo.listInvDocumentInterfaceByReference infoData = new YurticiInfo.listInvDocumentInterfaceByReference();
            infoData.userName = "kullanıcıAdınızıBurayaYazın";
            infoData.password = "şifreniziBurayaYazın";
            infoData.language = "TR";

            infoData.fieldName = "54"; // İrsaliye özel alanına göre ara diyoruz
            infoData.fieldValueArray = new string[] { "236201", "GG111222341"}; // Birden fazla referans değeri ile sorgu yapabiliyoruz
            infoData.withCargoLifecycle = "0";  // İlgili kargoların hangi datalarını almak istediğimizi söylüyoruz. 

            YurticiInfo.CustParamsVO voInfo = new YurticiInfo.CustParamsVO();
            //Yurtiçikargo sisteminde kayıtlı ödeyecek Müşteri kodudur.
            voInfo.invCustIdArray = new string[] { "986665122" };//(Aracı firma olduğumuz için ben kendi müşteri kodumu yazdım buraya)
            infoData.custParamsVO = voInfo;

            var result = client.listInvDocumentInterfaceByReference(infoData);

            textBox1.Text = result.ShippingDataResponseVO.outResult;
        }

        private void button3_Click(object sender, EventArgs e) {
            ServiceReference1.ShippingOrderDispatcherServicesClient client = new ServiceReference1.ShippingOrderDispatcherServicesClient();

            ServiceReference1.queryShipment query = new ServiceReference1.queryShipment();
            query.wsUserName = "kullanıcıAdınızıBurayaYazın";
            query.wsPassword = "şifreniziBurayaYazın";
            query.wsLanguage = "TR";
            query.keys = new string[] { "236201", "GG111222341" };
            query.keyType = 0;
            query.addHistoricalData = true;
            query.onlyTracking = true;

            var result = client.queryShipment(query);
            textBox1.Text = result.ShippingDeliveryVO.outResult;
        }
    }
}
