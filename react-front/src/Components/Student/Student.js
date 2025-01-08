import './Student.css';
import axios from 'axios'
import { useState } from 'react';

function Student({ title }) {
  const [students, setStudents] = useState([]);
  const [matricula, setMatricula] = useState('');

  const handleMatricula = (e) => setMatricula(e.target.value);

  let getStudents = () => {
    axios.get(`http://localhost:5112/api/Students/${matricula}`)
      .then(response => {
        let studentData = []
        response.data.forEach((data, index) => {
          studentData.push(
            <div className="row" key={index}>
              <div className="col">
                {data.matricula}
              </div>
              <div className="col">
                {data.tipoPago}
              </div>
              <div className="col">
                {data.institucion}
              </div>
            </div>
          )
        })
        setStudents(studentData);
      })
      .catch(error => {
        if (error.response.status === 404) {
          setStudents([])
          alert('No se encontraron resultados!')
        } else {
          alert('Error en el servicio!')
        }
      })
  }


  return (
    <div className="container text-center">
      <h1 className="display-1">{title}</h1>
      <div className="row">
        <h1 className="display-6 float-end">Búsqueda por Matrícula:</h1>
      </div>
      <div className="row offset-3 col-6 center">
        <div className="input-group mb-3">
          <input onChange={handleMatricula} type="text" className="form-control" placeholder="Matrícula" aria-label="Matricula"
            aria-describedby="button-addon2" id="matriculaStudent" />
          <button className="btn btn-outline-primary" type="button" id="button-addon2"
            onClick={getStudents}>Buscar</button>
        </div>
      </div>

      <hr />

      <div className="row">
        <div className="col fw-bold">
          Matrícula
        </div>
        <div className="col fw-bold">
          Tipo de Pago
        </div>
        <div className="col fw-bold">
          Institución
        </div>
      </div>

      <hr />

      {students}
    </div>
  );
}

export default Student;
