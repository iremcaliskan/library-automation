$(document).on("click", "#girisYap", function () {
    $(this).html("Kontrol ediliyor...");
    $(this).prop("disabled", true);

    var degerler = {
        email: $("#email").val(),
        sifre: $("#sifre").val(),
        hatirla: false
    };

    if ($("#checkbox-signup").is(":checked")) {
        degerler.hatirla = true;
    }

    $.ajax({
        type: 'Post',
        url: '/Giris/GirisKontrolJson',
        data: JSON.stringify(degerler),
        dataType: 'Json',
        contentType: 'application/json;charset=utf-8',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    icon: 'success', title: 'Giriş Başarılı', text: "Yönlendiriliyorsunuz!" });
                window.location.href = '/Kitap/Index';
            }
            else if (gelenDeg == "BosOlamaz") {
                Swal.fire({
                    icon: 'error', title: 'Giriş Başarısız', text: "Gerekli alanları doldurunuz!" });
            } else {
                Swal.fire({
                    icon: 'error', title: 'Giriş Başarısız', text: "Bilgilerinizi kontrol ediniz!" });
            }
        },
        error: function() {
            Swal.fire({
                icon: 'error', title: 'Hata', text: "Giriş yapılırken hata oluştu!" });
        },
        complete: function() {
            $("#girisYap").html("Giriş Yap");
            $("#girisYap").prop("disabled", false);
        }
    });
});
