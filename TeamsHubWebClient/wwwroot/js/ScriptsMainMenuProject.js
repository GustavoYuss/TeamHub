
// Referencias para los modales
const addActivityBtn = document.getElementById("addActivityBtn");
const modal = document.getElementById("activityForm");
const closeModal = document.querySelectorAll(".close"); // Para cerrar múltiples modales
const backBtn = document.querySelector(".back-btn");
const detailModal = document.getElementById("detailModal");
const activityDetails = document.getElementById("activityDetails");
const statusCombo = document.getElementById("statusCombo");

addActivityBtn.addEventListener("click", () => {
    modal.style.display = "block";
});

closeModal.forEach((element) => {
    element.addEventListener("click", () => {
        modal.style.display = "none";
        detailModal.style.display = "none"; // Cerrar ambos modales
    });
});

window.addEventListener("click", (event) => {
    if (event.target === modal || event.target === detailModal) {
        modal.style.display = "none";
        detailModal.style.display = "none";
    }
});

backBtn.addEventListener("click", () => {
    alert("Volver a la ventana anterior"); // Aquí puedes implementar la lógica para retroceder
});

const newActivityForm = document.getElementById("newActivityForm");
newActivityForm.addEventListener("submit", (e) => {
    e.preventDefault();

    const activityName = document.getElementById("activityName").value;
    const startDate = document.getElementById("startDate").value;
    const endDate = document.getElementById("endDate").value;
    const responsible = document.getElementById("responsible").value;

    const newCard = document.createElement("div");
    newCard.classList.add("activity-card");
    newCard.innerHTML = `
        <h4>${activityName}</h4>
        <p>Responsable: ${responsible}</p>
        <button class="view-details">Ver detalles</button>
    `;

    document.getElementById("pendingActivities").appendChild(newCard);

    // Agregar evento para el botón "Ver detalles"
    newCard.querySelector(".view-details").addEventListener("click", () => {
        detailModal.style.display = "block";

        // Mostrar detalles
        activityDetails.innerText = `Nombre: ${activityName}\nResponsable: ${responsible}\nFecha de inicio: ${startDate}\nFecha de cierre: ${endDate}`;

        // Cambiar el estado
        statusCombo.value = "pending"; // Estado por defecto
        statusCombo.addEventListener("change", (e) => {
            const newState = e.target.value;
            
            // Mover la tarjeta a la columna correspondiente
            newCard.remove(); // Eliminar de la columna actual
            if (newState === "in-progress") {
                document.getElementById("inProgressActivities").appendChild(newCard);
            } else if (newState === "completed") {
                document.getElementById("completedActivities").appendChild(newCard);
            } else {
                document.getElementById("pendingActivities").appendChild(newCard);
            }
        });
    });

    modal.style.display = "none";
    newActivityForm.reset(); // Restablecer el formulario
});
