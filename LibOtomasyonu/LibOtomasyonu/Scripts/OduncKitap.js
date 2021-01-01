$(document).on("click", "#kitapVer", function () {
    var degerler = {
        uyeId: $("#uyeId option:selected").attr("data-id"),
        kitapId: $("#kitapId option:selected").attr("data-id"),
        getirecegiTarih: $("#getirecegiTarih").val()
    };

    $.ajax({
        type: 'Post',
        url: '/OduncKitap/KitapVerJson',
        data: JSON.stringify(degerler),
        dataType: 'JSON',
        contentType: 'application/json;charset=utf-8',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Kitap Verildi',
                    text: 'İşlem  başarılı!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Kitap Verilmedi',
                    text: 'Veritabanına eklenirken bir sorun ile karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Kitap Verilmedi',
                text: 'İşlem  başarısız!'
            });
        }
    });
});

$(document).on("click", "#verilenKitabiGuncelle", function () {
    var degerler = {
        oduncKitapId: $(this).attr("data-id"),
        uyeId: $("#uyeId option:selected").attr("data-id"),
        kitapId: $("#kitapId option:selected").attr("data-id"),
        getirecegiTarih: $("#getirecegiTarih").val()
    };

    $.ajax({
        type: 'Post',
        url: '/OduncKitap/VerilenKitabiGuncelleJson',
        data: JSON.stringify(degerler),
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (gelenDeg) {
            if (gelenDeg == "1") {
                Swal.fire({
                    type: 'success',
                    title: 'Verilen Kitap Güncellendi',
                    text: 'İşlem  başarılı!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Verilen Kitap Güncellenemedi',
                    text: 'Veritabanına eklenirken bir sorun ile karşılaşıldı!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Verilen Kitap Güncellenemedi',
                text: 'İşlem  başarısız!'
            });
        }
    });
});

$(document).on("click", ".getirildiOlarakIsaretle", function () {
    Swal.fire({
        title: 'İşaretleniyor...',
        text: "Getirildi olarak işaretlemek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Tamam',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {
            var oduncKitapId = $(this).val();
            var tr = $(this).parent("td").parent("tr");

            $.ajax({
                type: 'Post',
                url: '/OduncKitap/GetirdiIsaretle',
                data: { "oduncKitapId": oduncKitapId },
                dataType: 'Json',
                success: function (data) {
                    if (data == "1") {
                        tr.remove();
                        Swal.fire({
                            type: 'success',
                            title: 'Getirildi olarak işaretlendi!',
                            text: 'İşlem başarıyla gerçekleşti!'
                        });
                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: 'Getirildi olarak işaretlenemedi!',
                            text: 'Veritabanında bir sorun oluştu!'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        type: 'error',
                        title: 'Getirildi olarak işaretlenemedi!',
                        text: 'Bir sorun oluştu!'
                    });
                }
            });
        }
    })
});