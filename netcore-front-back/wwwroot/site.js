function getStudents() {
    const matricula = document.querySelector('#matriculaStudent').value
    fetch(`api/Students/${matricula}`)
        .then(response => response.json())
        .then(data => _displayStudents(data))
        .catch(error => alert('Error en el servicio!'))
}

function _displayStudents(data) {
    let dataGrid = document.querySelector('#studentsData')
    dataGrid.innerHTML = ''

    if (!data.length) {
        alert('No se encontraron resultados!')
    } else {
        data.forEach(item => {
            const dataRow = `
                <div class="row">
                    <div class="col">
                        ${item.matricula}
                    </div>
                    <div class="col">
                        ${item.tipoPago}
                    </div>
                    <div class="col">
                        ${item.institucion}
                    </div>
                </div>
            `
            dataGrid.innerHTML += dataRow
        })
    }

}