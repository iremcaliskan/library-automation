//Kitap ekleme işlemi içerisine kategorileri çekme
$(document).on("click", "#kategoriEkle", function () {
    var secilenKategoriAd = $("#kategoriler").val();
    var secilenId = $("#kategoriler option:selected").attr("data-id");

    if (secilenKategoriAd != null && secilenKategoriAd != "") {
        $("#eklenenKategoriler").append('<div id="' + secilenId + '" class="col-md-1 bg-primary kategoriSil" style="margin-right:2px; margin-bottom:2px;">' + secilenKategoriAd + '</div>');
        $("#kategoriler option:selected").remove();
    }
});

//Kitap ekleme işlemi içerisinde eklenen kategorileri silme
$(document).on("click", ".kategoriSil", function () {
    var id = $(this).attr("id");
    var kategoriAd = $(this).html();
    $("#kategoriler").append('<option data-id=" ' + id + '">' + kategoriAd + '</option>');
    $(this).remove();
});

//Kitap ekleme işlemi içerisinde kitabı kaydetme
$(document).on("click", "#kitapKaydet", function () {
    var degerler = {
        kategoriler: [],
        yazar: $("#yazar option:selected").attr("data-id"),
        kitapAd: $("#kitapAd").val(),
        kitapAdet: $("#kitapAdet").val(),
        siraNo:  $("#siraNo").val()
    };

    $("#eklenenKategoriler div").each(function () {
        var id = $(this).attr("id");
        degerler.kategoriler.push(id);
    });

    $.ajax({
        type: 'Post',
        url: '/Kitap/EkleJson',
        data: JSON.stringify(degerler),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Kitap Eklendi',
                    text: 'İşlem  başarılı!'
                });
            }else if ("bosOlamaz") {
                Swal.fire({
                    type: 'error',
                    title: 'Kitap Eklenmedi',
                    text: 'Boş alanları doldurunuz!'
                });
            }
        },
        error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Kitap Eklenmedi',
                    text: 'İşlem  başarısız!'
                });
        }
    });
});

//Eklenen kitabı silme
$(document).on("click", ".ktpSil", function () {
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Silinme işlemi geri alınamaz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            var kitapId = $(this).val();
            var tr = $(this).parent("td").parent("tr");

            $.ajax({
                type: 'Post',
                url: '/Kitap/SilJson',
                data: { "kitapId": kitapId },
                dataType: 'Json',
                success: function (data) {
                    if (data == "1") {
                        tr.remove();
                        Swal.fire({
                            type: 'success',
                            title: 'Kitap Silindi',
                            text: 'İşlem başarıyla gerçekleşti!'
                        });
                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: 'Kitap Silinemedi',
                            text: 'Veritabanında bir sorun oluştu!'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        type: 'error',
                        title: 'Kitap Silinemedi',
                        text: 'Bir sorun oluştu!'
                    });
                }
            });
        }
    })
});

//Eklenen kitabı güncelleme
$(document).on("click", "#kitapGuncelle", function () {
    var degerler = {
        kategoriler: [],
        yazar: $("#yazar option:selected").attr("data-id"),
        kitapAd: $("#kitapAd").val(),
        kitapAdet: $("#kitapAdet").val(),
        siraNo: $("#siraNo").val(),
        kitapId: $(this).attr("data-id")
    };

    $("#eklenenKategoriler div").each(function () {
        var id = $(this).attr("id");
        degerler.kategoriler.push(id);
    });

    $.ajax({
        type: 'Post',
        url: '/Kitap/GuncelleJson',
        data: JSON.stringify(degerler),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Kitap Güncellendi',
                    text: 'İşlem  başarılı!'
                });
            } else if ("bosOlamaz") {
                Swal.fire({
                    type: 'error',
                    title: 'Kitap Güncellenemedi',
                    text: 'Boş alanları doldurunuz!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kitap Güncellenemedi',
                text: 'İşlem  başarısız!'
            });
        }
    });
});