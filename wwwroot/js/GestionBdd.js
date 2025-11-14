document.addEventListener("DOMContentLoaded", function () {

    // Fonction générique pour gérer un bouton avec data-url et optional data-body (via inputs)
    function bindButton(idBtn, idSpinner, idResult, options = {}) {
        const btn = document.getElementById(idBtn);
        if (!btn) return;

        btn.addEventListener("click", async function () {
            const spinner = document.getElementById(idSpinner);
            const resultat = document.getElementById(idResult);

            spinner.style.display = "inline-block";
            btn.disabled = true;
            resultat.textContent = "";

            const url = btn.dataset.url;
            const method = btn.dataset.method || "POST";

            // Si tu veux récupérer des inputs pour le body, tu peux l'ajouter via data-body-ids, etc.
            // Pour l'exemple on envoie {} au endpoint insert qui attend un JSON.
            let body = {}; // par défaut body vide

            //Si on clique sur le bouton d'insertion, on lit les champs Nom + Password
            if (btn.id === "btnInsertBdd") {
                body = {
                    nom: document.getElementById("nomInsert").value.trim(),
                    password: document.getElementById("passwordInsert").value.trim()
                };
            }

            try {
                const resp = await fetch(url, {
                    method: method,
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: method === "GET" ? null : JSON.stringify(body)
                });

                const json = await resp.json();

                spinner.style.display = "none";
                btn.disabled = false;

                resultat.textContent = json.message || (json.succes ? "OK" : "Erreur");

            } catch (err) {
                spinner.style.display = "none";
                btn.disabled = false;
                resultat.textContent = "❌ Erreur : " + err;
            }
        });
    }

    bindButton("btnTestBdd", "spinnerTest", "resultatTest");
    bindButton("btnInsertBdd", "spinnerInsert", "resultatInsert");
});


