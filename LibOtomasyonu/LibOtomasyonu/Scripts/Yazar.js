//Yazar Ekleme
$(document).on("click", "#yazarEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: 'Yazar Ekle',
        html:
            '<input type="text" id="yzrAd" class="swal2-input">'
    })

    if (formValues) {
        var yzrAd = $("#yzrAd").val();
        //almış olduğumuz değeri kontroller kısmına gönderip kaydetme
        $.ajax({
            type: 'Post',
            url: '/Yazar/EkleJson',
            data: { "yzrAd": yzrAd },
            dataType: 'JSON',
            success: function (data) {
                var yzrId = data.Result.Id;
                var yzrAd = '<td>' + data.Result.Ad + '</td>';
                var guncelleButon = "<td><button class='guncelle btn btn-custom' value='" + yzrId + "'>Güncelle</button></td>";
                var silButon = "<td><button class='sil btn btn-danger' value='" + yzrId + "'>Sil</button></td>";
                var kitapAdeti = "<td>0</td>";

                //verileri aldık tabloya ekleyecegiz
                $("#example tbody").prepend('<tr>' + yzrAd + kitapAdeti + guncelleButon + silButon + '</tr>');

                //mesaj kutusu
                Swal.fire({
                    type: 'success',
                    title: 'Yazar Eklendi',
                    text: 'İşlem başarıyla gerçekleşti!'
                });
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Yazar Eklenmedi',
                    text: 'İşlem  başarısız!'
                });
            }
        });
    }
});

//Yazar Güncelleme işlemleri
$(document).on("click", ".yazarGuncelle", async function () {
    var yzrId = $(this).val();
    var yzrAdTd = $(this).parent("td").parent("tr").find("td:first");
    var yzrAd = yzrAdTd.html();
    const { value: formValues } = await Swal.fire({
        title: 'Yazar Güncelle',
        html:
            '<input id="yzrAd" value="' + yzrAd + '" class="swal2-input">'
    })

    if (formValues) {
        yzrAd = $("#yzrAd").val();
        //Controller'a gönderip kayıt edilecek
        $.ajax({
            type: 'Post',
            url: '/Yazar/GuncelleJson',
            data: { "yzrId": yzrId, "yzrAd": yzrAd },
            dataType: 'Json',
            success: function (data) {
                if (data == "1") {
                    //mesaj kutusu
                    yzrAdTd.html(yzrAd);
                    Swal.fire({
                        type: 'success',
                        title: 'Yazar Güncellendi',
                        text: 'İşlem başarıyla gerçekleşti!'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Yazar Güncellenemedi',
                        text: 'İşlem  başarısız!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Yazar Eklenmedi',
                    text: 'İşlem  başarısız!'
                });
            }
        });
    }
});

//Yazar Silme İşlemleri
$(document).on("click", ".yazarSil", async function () {
    var tr = $(this).parent("td").parent("tr").find("td");
    var yzrId = $(this).val();

    $.ajax({
        type: 'Post',
        url: '/Yazar/SilJson',
        data: { "yzrId": yzrId },
        dataType: 'Json',
        success: function (data) {
            if (data == "1") {
                tr.remove();
                Swal.fire({
                    type: 'success',
                    title: 'Yazar Silindi',
                    text: 'İşlem başarıyla gerçekleşti!'
                });
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Yazar Silinemedi',
                    text: 'İşlem  başarısız!'
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Yazar Silinemedi',
                text: 'İşlem  başarısız!'
            });
        }
    });
});