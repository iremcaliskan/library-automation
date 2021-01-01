//Üyelik İşlemleri
$(document).on("click", "#uyelikEkle", function () {
    var uyeId = $("#uyeId").val();
    var mail = $("#mail").val();
    var parola = $("#parola").val();
    var parolaTekrar = $("#parolaTekrar").val(); //Değerler alınıp ajax ile gönderilir

    $.ajax({
        type: 'Post',
        url: '/Uyelik/EkleJson',
        data: { 'uyeId': uyeId, 'mail': mail, 'parola': parola, 'parolaTekrar': parolaTekrar },
        dataType: 'JSON',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Üyelik Oluştu',
                    text: 'İşlem başarıyla gerçekleştirildi!'
                });
            }
            else if (gelenDeg == "bosOlamaz") {
                Swal.fire({
                    type: 'error',
                    title: 'Üyelik Oluşturulamadı',
                    text: 'Boş alanları doldurunuz!'
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
                title: 'Üyelik Oluşturulamadı',
                text: 'İşlem başarısız!'
            });
        }      
    });
});

$(document).on("click", "#uyelikGuncelle", function () {
    var uyeId = $("#uyeId").val();
    var mail = $("#mail").val();
    var parola = $("#parola").val();
    var parolaTekrar = $("#parolaTekrar").val(); //Değerler alınıp ajax ile gönderilir

    $.ajax({
        type: 'Post',
        url: '/Uyelik/GuncelleJson',
        data: { 'uyeId': uyeId, 'mail': mail, 'parola': parola, 'parolaTekrar': parolaTekrar },
        dataType: 'JSON',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Üyelik Güncellendi',
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
            else if (gelenDeg == "mailBosOlamaz") {
                Swal.fire({
                    type: 'error',
                    title: 'Üyelik Güncellenemedi',
                    text: 'Mail alanını doldurunuz!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Üyelik Güncellenemedi',
                text: 'İşlem başarısız!'
            });
        }
    });
});

$(document).on("click", ".uyelikSil", function () {
    var uyeId = $(this).val();
    var tr = $(this).parent("td").parent("tr");

    $.ajax({
        type: 'Post',
        url: '/Uyelik/SilJson',
        data: { 'uyeId': uyeId },
        dataType: 'JSON',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                tr.remove();
                Swal.fire({
                    type: 'success',
                    title: 'Üyelik Silindi',
                    text: 'İşlem başarıyla gerçekleştirildi!'
                });
            }
            else{
                Swal.fire({
                    type: 'error',
                    title: 'Üyelik Silinemedi',
                    text: 'Veritabanında bir sorun oluştu!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Üyelik Silinemedi',
                text: 'İşlem başarısız!'
            });
        }
    });
});

$(document).on("click", "#yetkiAta", async function () {
    var select = '<select id="yetkiId">' +
        '<option value="2">Moderatör</option>' +
        '<option value="3">İzleyici</option>' +
        '</select>';

    const { value: formValues } = await Swal.fire({
        title: 'Yetki Ata',
        html: select
    })

    var uyeId = $(this).attr("data-id");
    var yetkiId = $("#yetkiId").val();
    var yetkiAd = $("#yetkiId option:selected").text();
    var buton = $(this);

    $.ajax({
        type: 'Post',
        url: '/Uyelik/YetkiAtaJson',
        data: { 'uyeId': uyeId, 'yetkiId': yetkiId },
        dataType: 'JSON',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                buton.text(yetkiAd);
                Swal.fire({
                    type: 'success',
                    title: 'Yetki Atandı',
                    text: 'İşlem başarıyla gerçekleştirildi!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Yetki Atanamadı',
                    text: 'Veritabanında bir sorun oluştu!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Yetki Atanamadı',
                text: 'İşlem başarısız!'
            });
        }
    });
});
//Üyelik İşlemleri SON