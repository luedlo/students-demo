import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Student from './Components/Student/Student';
import logo from './assets/logo.svg';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <div className="container mx-auto">
      <img alt="React logo" src={logo} width="15%" className="row mx-auto" />
      <Student title="Sistema de Consultas v3.0" />
    </div>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
