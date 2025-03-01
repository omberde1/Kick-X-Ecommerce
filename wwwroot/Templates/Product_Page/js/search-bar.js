document.getElementById("searchInput").addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault(); // Prevent default Enter behavior
        let input = this.value.trim(); // Get the value and trim spaces

        if (input !== "") {
            document.getElementById("searchForm").submit(); // Submit only if input is not empty
        }
    }
});