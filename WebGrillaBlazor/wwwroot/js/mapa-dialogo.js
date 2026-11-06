window.mapaDialog = {

    abrir: function (dialog, referenciaBlazor) {

        dialog.addEventListener("close", function () {
            referenciaBlazor.invokeMethodAsync("AlCerrarMapa");
        }, { once: true });

        if (!dialog.open) {
            dialog.showModal();
        }
    },

    cerrar: function (dialog) {

        if (dialog.open) {
            dialog.close();
        }
    }
};