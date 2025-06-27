// Xử lý chuyển tab
$(document).ready(function () {
    $('.tab-button').click(function () {
        $('.tab-button').removeClass('active');
        $(this).addClass('active');

        const tab = $(this).data('tab');
        $('.tab-panel').removeClass('active');
        $('#' + tab).addClass('active');
    });

    // Dịch văn bản
    // Tự động dịch khi gõ xong (sau 800ms không gõ nữa)
    let typingTimer;
    $('#input-text').on('input', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(autoTranslate, 500);
    });

    function autoTranslate() {
        const text = $('#input-text').val().trim();
        const from = $('#from-language').val();
        const to = $('#to-language').val();

        if (text === "") return;

        $.ajax({
            url: '/api/text/translate',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                text: text,
                sourceLanguage: from,
                targetLanguage: to
            }),
            success: function (res) {
                $('#output-text').val(res.translatedText);
            },
            error: function (xhr) {
                $('#output-text').val("⚠️ Lỗi dịch: " + xhr.responseText);
            }
        });
    }


    // Xóa văn bản
    $('#clear-btn').click(function () {
        $('#input-text').val('');
        $('#output-text').val('');
    });

    // Đảo ngôn ngữ
    $('#swap-button').click(function () {
        const from = $('#from-language').val();
        $('#from-language').val($('#to-language').val());
        $('#to-language').val(from);
    });

    // Phát âm văn bản đã dịch
    $('#speak-btn').click(function () {
        const text = $('#output-text').val();
        const lang = $('#to-language').val();
        if (!text.trim()) {
            alert('Không có văn bản để phát âm.');
            return;
        }

        const synth = window.speechSynthesis;
        const utter = new SpeechSynthesisUtterance(text);
        utter.lang = lang;
        synth.speak(utter);
    });

    $('#document-translate-btn').click(function () {
        const file = $('#document-upload')[0].files[0];
        const fromLang = $('#from-language').val();
        const toLang = $('#to-language').val();

        if (!file) {
            alert("Vui lòng chọn tài liệu");
            return;
        }

        const formData = new FormData();
        formData.append('file', file);
        formData.append('from', fromLang);
        formData.append('to', toLang);

        $.ajax({
            url: '/api/document/translate',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                $('#document-translated-text').val(res.translatedText);
            },
            error: function (err) {
                alert("Dịch tài liệu thất bại");
                console.error(err);
            }
        });
    });

});
