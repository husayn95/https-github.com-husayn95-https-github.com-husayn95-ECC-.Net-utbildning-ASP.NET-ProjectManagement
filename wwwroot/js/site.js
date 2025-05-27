//Fick hjälp av chatgpt för att framställa endel av denna kod

document.addEventListener("DOMContentLoaded", function () {
  const profileBtn = document.getElementById("profileBtn");
  const dropdown = document.getElementById("profileDropdown");
  document.addEventListener("click", function (event) {
    const target = event.target;

    if (target.closest(".action-btn")) {
      event.preventDefault();
      const href = target.closest("a").getAttribute("href");
      if (href) {
        document.querySelector(".content-area").style.opacity = "0";
        setTimeout(() => {
          window.location.href = href;
        }, 300);
      }
    }
  });

  const dateInputs = document.querySelectorAll('input[type="date"]');
  dateInputs.forEach((input) => {
    if (!input.value) {
      const today = new Date().toISOString().split("T")[0];
      input.value = today;
    }
  });

  profileBtn.addEventListener("click", function (e) {
    e.preventDefault();

    const isHidden = dropdown.classList.contains("hidden");

    if (isHidden) {
      dropdown.classList.remove("hidden");
      profileBtn.setAttribute("aria-expanded", "true");
    } else {
      dropdown.classList.add("hidden");
      profileBtn.setAttribute("aria-expanded", "false");
    }
  });

  document.addEventListener("click", function (e) {
    if (!profileBtn.contains(e.target) && !dropdown.contains(e.target)) {
      dropdown.classList.add("hidden");
      profileBtn.setAttribute("aria-expanded", "false");
    }
  });
});
