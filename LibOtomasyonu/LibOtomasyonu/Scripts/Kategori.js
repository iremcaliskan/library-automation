//Kategori Ekleme
$(document).on("click", "#ktgEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: 'Kategori Ekle',
        html:
            '<input type="text" id="ktgAd" class="swal2-input">'
    })

    if (formValues) {
        var ktgAd = $("#ktgAd").val();
        //almış olduğumuz değeri kontroller kısmına gönderip kaydetme
        $.ajax({
            type: 'Post',
            url: '/Kategori/EkleJson',
            data: { "ktgAd": ktgAd },
            dataType: 'Json',
            success: function (data) {
                var ktgId = data.Result.Id;
                var ktgAd = '<td>' + data.Result.Ad + '</td>';
                var kitapAdeti = "<td>0</td>";
                var guncelleButon = "<td><button class='guncelle btn btn-custom' value='" + ktgId + "'>Güncelle</button></td>";
                var silButon = "<td><button class='sil btn btn-danger' value='" + ktgId + "'>Sil</button></td>";

                //verileri aldık tabloya ekleyecegiz
                $("#example tbody").prepend('<tr>' + ktgAd + kitapAdeti + guncelleButon + silButon + '</tr>');

                //mesaj kutusu
                Swal.fire({
                    type: 'success',
                    title: 'Kategori Eklendi',
                    text: 'İşlem başarıyla gerçekleşti!'
                });
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Kategori Eklenmedi',
                    text: 'İşlem  başarısız!'
                });
            }
        });
    }
});

//Kategori Güncelleme işlemleri
$(document).on("click", ".ktgGuncelle", async function () {
    var ktgId = $(this).val();
    var ktgAdTd = $(this).parent("td").parent("tr").find("td:first");
    var ktgAd = ktgAdTd.html();
    const { value: formValues } = await Swal.fire({
        title: 'Kategori Güncelle',
        html:
            '<input type="text" id="ktgAd" value="' + ktgAd + '" class="swal2-input">'
    })

    if (formValues) {
        ktgAd = $("#ktgAd").val();
        //Controller'a gönderip kayıt edilecek
        $.ajax({
            type: 'Post',
            url: '/Kategori/GuncelleJson',
            data: { "ktgId": ktgId, "ktgAd": ktgAd },
            dataType: 'Json',
            success: function (data) {
                if (data == "1") {
                    //mesaj kutusu
                    ktgAdTd.html(ktgAd);
                    Swal.fire({
                        type: 'success',
                        title: 'Kategori Güncellendi',
                        text: 'İşlem başarıyla gerçekleşti!'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Kategori Güncellenemedi',
                        text: 'İşlem  başarısız!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Kategori Eklenmedi',
                    text: 'İşlem  başarısız!'
                });
            }
        });
    }
});

//Kategori Silme İşlemleri
$(document).on("click", ".ktgSil", async function () {
    var ktgId = $(this).val();
    var tr = $(this).parent("td").parent("tr").find("td");

        $.ajax({
            type: 'Post',
            url: '/Kategori/SilJson',
            data: { "ktgId": ktgId},
            dataType: 'Json',
            success: function (data) {
                if (data == "1") {
                    tr.remove();
                    Swal.fire({
                        type: 'success',
                        title: 'Kategori Silindi',
                        text: 'İşlem başarıyla gerçekleşti!'
                    });
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: 'Kategori Silinemedi',
                        text: 'İşlem  başarısız!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    type: 'error',
                    title: 'Kategori Silinemedi',
                    text: 'İşlem  başarısız!'
                });
            }
        });
});