//Fick hjälp av chatgpt för att framställa denna fil.

class FormValidator {
  constructor() {
    this.rules = {};
    this.init();
  }

  init() {
    document.addEventListener("DOMContentLoaded", () => {
      this.setupLoginValidation();
      this.setupRegisterValidation();
      this.setupProjectValidation();
    });
  }

  addRule(fieldId, rules) {
    this.rules[fieldId] = rules;
    this.setupFieldValidation(fieldId);
  }

  setupFieldValidation(fieldId) {
    const field = document.getElementById(fieldId);
    if (!field) return;

    field.addEventListener("blur", () => this.validateField(fieldId));
    field.addEventListener("input", () => this.validateField(fieldId));
  }

  validateField(fieldId) {
    const field = document.getElementById(fieldId);
    const rules = this.rules[fieldId];
    const errorElement = document.getElementById(fieldId + "Error");
    const successElement = document.getElementById(fieldId + "Success");

    if (!field || !rules) return true;

    const value = field.value.trim();

    for (const rule of rules) {
      if (!rule.test(value)) {
        this.showError(field, errorElement, successElement, rule.message);
        return false;
      }
    }

    this.showSuccess(field, errorElement, successElement, "Valid");
    return true;
  }

  showError(field, errorElement, successElement, message) {
    field.classList.remove("valid");
    field.classList.add("error");

    if (errorElement) {
      errorElement.textContent = message;
      errorElement.classList.add("show");
    }

    if (successElement) {
      successElement.classList.remove("show");
    }
  }

  showSuccess(field, errorElement, successElement, message) {
    field.classList.remove("error");
    field.classList.add("valid");

    if (errorElement) {
      errorElement.classList.remove("show");
    }

    if (successElement) {
      successElement.textContent = message;
      successElement.classList.add("show");
    }
  }

  validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return false;

    let isValid = true;
    const inputs = form.querySelectorAll("input, textarea, select");

    inputs.forEach((input) => {
      if (this.rules[input.id]) {
        if (!this.validateField(input.id)) {
          isValid = false;
        }
      }
    });

    return isValid;
  }

  setupLoginValidation() {
    this.addRule("loginEmail", [
      { test: (val) => val.length > 0, message: "Email is required" },
      {
        test: (val) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val),
        message: "Please enter a valid email address",
      },
    ]);

    this.addRule("loginPassword", [
      { test: (val) => val.length > 0, message: "Password is required" },
      {
        test: (val) => val.length >= 6,
        message: "Password must be at least 6 characters long",
      },
    ]);

    const loginForm = document.getElementById("loginForm");
    if (loginForm) {
      loginForm.addEventListener("submit", (e) => {
        e.preventDefault();
        if (this.validateForm("loginForm")) {
          e.target.submit();
        } else {
          alert("Please fix the errors before submitting.");
        }
      });
    }
  }

  setupRegisterValidation() {
    this.addRule("registerFullName", [
      { test: (val) => val.length > 0, message: "Full name is required" },
      {
        test: (val) => val.length >= 2,
        message: "Full name must be at least 2 characters",
      },
    ]);

    this.addRule("registerEmail", [
      { test: (val) => val.length > 0, message: "Email is required" },
      {
        test: (val) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val),
        message: "Please enter a valid email address",
      },
    ]);

    this.addRule("registerPassword", [
      { test: (val) => val.length > 0, message: "Password is required" },
      {
        test: (val) => val.length >= 8,
        message: "Password must be at least 8 characters long",
      },
      {
        test: (val) => /(?=.*[a-z])/.test(val),
        message: "Password must contain at least one lowercase letter",
      },
      {
        test: (val) => /(?=.*[A-Z])/.test(val),
        message: "Password must contain at least one uppercase letter",
      },
      {
        test: (val) => /(?=.*\d)/.test(val),
        message: "Password must contain at least one number",
      },
    ]);

    const registerForm = document.getElementById("registerForm");
    if (registerForm) {
      registerForm.addEventListener("submit", (e) => {
        e.preventDefault();
        if (this.validateForm("registerForm")) {
          e.target.submit();
        } else {
          alert("Please fix the errors before submitting.");
        }
      });
    }
  }

  setupProjectValidation() {
    this.addRule("projectName", [
      { test: (val) => val.length > 0, message: "Project name is required" },
      {
        test: (val) => val.length >= 3,
        message: "Project name must be at least 3 characters",
      },
    ]);

    this.addRule("clientName", [
      { test: (val) => val.length > 0, message: "Client name is required" },
      {
        test: (val) => val.length >= 2,
        message: "Client name must be at least 2 characters",
      },
    ]);

    this.addRule("description", [
      { test: (val) => val.length > 0, message: "Description is required" },
      {
        test: (val) => val.length >= 10,
        message: "Description must be at least 10 characters",
      },
    ]);

    this.addRule("startDate", [
      { test: (val) => val.length > 0, message: "Start date is required" },
    ]);

    this.addRule("endDate", [
      { test: (val) => val.length > 0, message: "End date is required" },
      {
        test: (val) => {
          const endDate = new Date(val);
          const startDateField = document.getElementById("startDate");
          if (startDateField && startDateField.value) {
            const startDate = new Date(startDateField.value);
            return endDate > startDate;
          }
          return true;
        },
        message: "End date must be after start date",
      },
    ]);

    this.addRule("budget", [
      { test: (val) => val.length > 0, message: "Budget is required" },
      {
        test: (val) => !isNaN(val) && parseFloat(val) > 0,
        message: "Budget must be a positive number",
      },
    ]);

    const projectForm = document.getElementById("projectForm");
    if (projectForm) {
      projectForm.addEventListener("submit", (e) => {
        e.preventDefault();
        if (this.validateForm("projectForm")) {
          e.target.submit();
        } else {
          alert("Please fix the errors before submitting.");
        }
      });
    }
  }
}

const validator = new FormValidator();
