// src/components/Modal.js

import React from "react";
import "./Modal.css";

// Component bir fonksiyon olarak tanımlanıyor
function Modal({ isOpen, onClose, children }) {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <button className="modal-close-button" onClick={onClose}>
          &times;
        </button>
        {children}
      </div>
    </div>
  );
}

// Component varsayılan olarak export ediliyor
export default Modal;
