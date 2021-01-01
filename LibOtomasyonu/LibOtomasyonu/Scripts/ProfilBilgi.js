$(document).on("click", "#profilBilgiGuncelle", function () {
    var parola = $("#parola").val();
    var parolaTekrar = $("#parolaTekrar").val(); //Değerler alınıp ajax ile gönderilir
    var tel = $("#tel").val();

    $.ajax({
        type: 'Post',
        url: '/Uyelik/ProfilBilgiGuncelleJson',
        data: { 'parola': parola, 'parolaTekrar': parolaTekrar, 'tel': tel },
        dataType: 'JSON',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Profil Bilgileri Güncellendi',
                    text: 'İşlem başarıyla gerçekleştirildi!'
                });
            }
            else if (gelenDeg == "parolaUyusmazligi") {
                Swal.fire({
                    type: 'error',
                    title: 'Şifreler Uyuşmuyor',
                    text: 'Şifreler aynı değil!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Profil Bilgileri Güncellenemedi',
                text: 'İşlem başarısız!'
            });
        }
    });
});
