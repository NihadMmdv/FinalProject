console.log("car-update.js loaded");
document.addEventListener("DOMContentLoaded", function () {
    console.log("DOM fully loaded and parsed"); // Debugging

    // DOM Elements
    const brandInput = document.querySelector("#brandInput");
    const modelInput = document.querySelector("#modelInput");
    const yearInput = document.querySelector(".yearInput");
    const modelInputNone = document.querySelector(".modelInputNone");
    const dateDropdown = document.getElementById("date-dropdown");
    const sliders = document.querySelectorAll(".sliderMain");
    const setasmains = document.querySelectorAll(".setasmain");
    const removeImages = document.querySelectorAll(".removeimage");

    // Constants
    const currentYear = new Date().getFullYear();
    const earliestYear = 2000;
    const apiUrl = `/car/GetAllModel`;

    // Function to resolve circular references
    function resolveReferences(data) {
        const cache = new Map();

        function resolve(obj) {
            if (obj && obj.$ref) {
                return cache.get(obj.$ref);
            }
            if (obj && typeof obj === 'object') {
                if (obj.$id) {
                    cache.set(obj.$id, obj);
                }
                for (const key in obj) {
                    obj[key] = resolve(obj[key]);
                }
            }
            return obj;
        }

        return resolve(data);
    }

    // Fetch models from the API
    async function fetchModels() {
        try {
            const response = await fetch(apiUrl);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const data = await response.json();

            // Resolve circular references
            const resolvedData = resolveReferences(data);

            // Extract the models array
            return resolvedData.$values || [];
        } catch (error) {
            console.error("Error fetching models:", error);
            alert("Failed to load car models. Please check the console.");
            return [];
        }
    }

    // Update the models dropdown based on the selected brand
    function updateModels(models, selectedBrandId) {
        modelInput.innerHTML = ""; // Clear previous options

        // Add default "Select a Model" option
        const defaultOption = document.createElement("option");
        defaultOption.value = "";
        defaultOption.textContent = "Select a Model";
        modelInput.appendChild(defaultOption);

        // Filter models for the selected brand
        const filteredModels = models.filter(model => {
            return model.brandId == selectedBrandId; // Use loose equality
        });

        if (filteredModels.length === 0) {
            console.warn("No models found for brand ID:", selectedBrandId);
            return;
        }

        // Populate the dropdown with filtered models
        filteredModels.forEach(model => {
            const option = document.createElement("option");
            option.value = model.id;
            option.textContent = model.name;
            modelInput.appendChild(option);
        });
    }

    // Populate the year dropdown
    function populateYearDropdown() {
        for (let year = currentYear; year >= earliestYear; year--) {
            const option = document.createElement("option");
            option.value = year;
            option.textContent = year;
            if (year == yearInput.value) option.selected = true;
            dateDropdown.appendChild(option);
        }
    }

    // Handle slider toggles
    function handleSliders() {
        sliders.forEach(slider => {
            if (slider.closest(".activ")) {
                slider.style.setProperty("--boxAfterBackColor", "#3a4b39");
                slider.style.setProperty("--boxAfterTranslate", "translateX(1.9em)");
                slider.style.setProperty("--boxAfterButtonColor", "#84da89");
            }

            slider.addEventListener("click", () => {
                sliders.forEach(s => {
                    s.style.setProperty("--boxAfterBackColor", "#313033");
                    s.style.setProperty("--boxAfterTranslate", "translateX(0)");
                    s.style.setProperty("--boxAfterButtonColor", "#aeaaae");
                });
                slider.style.setProperty("--boxAfterBackColor", "#3a4b39");
                slider.style.setProperty("--boxAfterTranslate", "translateX(1.9em)");
                slider.style.setProperty("--boxAfterButtonColor", "#84da89");
            });
        });
    }

    // Handle "Set as Main" button clicks
    function handleSetAsMainButtons() {
        setasmains.forEach(set => {
            set.addEventListener("click", async (e) => {
                e.preventDefault();
                const endpoint = set.getAttribute("href");

                try {
                    const response = await fetch(endpoint);
                    const result = await response.json();

                    if (result.status === 200) {
                        Swal.fire("Good job!", "Successfully changed main photo!", "success");
                    } else if (result.status === 404) {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Something went wrong!",
                            footer: "<a href=''>Why do I have this issue?</a>",
                        });
                    }
                } catch (error) {
                    console.error("Error setting main photo:", error);
                }
            });
        });
    }

    // Handle "Remove Image" button clicks
    function handleRemoveImageButtons() {
        removeImages.forEach(img => {
            img.addEventListener("click", async (e) => {
                e.preventDefault();
                const endpoint = img.getAttribute("href");

                try {
                    const response = await fetch(endpoint);
                    const result = await response.json();

                    if (result.status === 200) {
                        Swal.fire("Good job!", "Successfully removed!", "success");
                        img.closest(".col-sm").remove(); // Remove the image container
                    } else if (result.status === 404 || result.status === 400) {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: result.desc || "Something went wrong!",
                        });
                    }
                } catch (error) {
                    console.error("Error removing image:", error);
                }
            });
        });
    }

    // Initialize the form
    async function initializeForm() {
        console.log("initializeForm called"); // Debugging
        const models = await fetchModels();

        if (models.length > 0) {
            // Find the selected model's brand ID
            const selectedModel = models.find(model => model.id == modelInputNone.value);
            if (selectedModel) {
                const selectedBrandId = selectedModel.brandId;

                // Set the selected brand in the dropdown
                const brandOption = brandInput.querySelector(`option[value="${selectedBrandId}"]`);
                if (brandOption) brandOption.selected = true;

                // Initial population of models dropdown
                updateModels(models, selectedBrandId);
            }

            // Update models dropdown when brand changes
            brandInput.addEventListener("change", () => {
                updateModels(models, brandInput.value);
            });
        } else {
            console.error("No models found.");
        }
    }

    // Call all initialization functions
    initializeForm();
    populateYearDropdown();
    handleSliders();
    handleSetAsMainButtons();
    handleRemoveImageButtons();
});