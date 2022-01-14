# ShopApi 

> .Net Core 5.0 ile geliştirildi.

> **Sqlite** database kullanıldı.

> **Respository Desing Pattern** ve **Unit Of Work** kullanıldı.

> Uygulamayı ayağa kaldırırken herhangi bir işlem yapılmasına gerek yok, sqlite db herhangi bir
konfigürasyon yapmadan çalışacaktır.

> Api *http://localhost:5000/swagger/index.html#/* üzerinden yada sizin makinenizde port üzerinden çalıştırabilir.

> Müşteri tablosu detayla şekilde UML şemasını ekledim. Müşterini Mağaza çalışanı yada Mağazaya bağlı olma durumu tablodu bir flag olarak belirlenmiştir. Fatura hesaplama işleminde bu flaga bakılarak alacağı indirim oranı belirlenir.

> İndirim Tablosu Mağaza çalışanı, mağazaya bağlı, 2 yıldan fazladır müşteri olma durumu (Customer tablosundaki InsertDate ile hesaplanır) ve indirim almama durumları bu tablodan çekilir.

> Fatura Tablosu, oluşturulan tüm faturalar müşteri ile ilişkili olmak zorundadır. UI tarafından bize gönderilen 
* Müşteri Id(CustomerId)i
* işlem Tipi [Mağazandan mı yapılmış] (IsShoppingAction)  - bu flag false ise işlem mağaza dışında yapılmış yani %'li indirimlerden yararlanabilir
* İşlem tutarı(Price) 

değerleri alınır ve müşterinin duruma göre ne kadarlık indirim alacağı hesaplanır daha işlem tutarının 100'e göre modunun 5 ile çarpımı kadar ek indirim yapılarak sonuç fatura model olarak döner.


> Çağlar KARABACAK

