import React, { useEffect, useState } from "react";
import api from "../api/axios";

function NakliyeYonetimi() {
  const [nakliyeler, setNakliyeler] = useState([]);
  const [form, setForm] = useState({
    firmaId: "",
    sahisId: "",
    nakliyeUcreti: "",
  });
  const [error, setError] = useState("");

  useEffect(() => {
    api
      .get("/nakliye")
      .then((res) => setNakliyeler(res.data))
      .catch(() => setError("Nakliye verisi yüklenemedi."));
  }, []);

  const handle = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    api
      .post("/nakliye", {
        ...form,
        nakliyeUcreti: parseFloat(form.nakliyeUcreti),
      })
      .then((res) => {
        setNakliyeler([...nakliyeler, res.data]);
        setForm({ firmaId: "", sahisId: "", nakliyeUcreti: "" });
      })
      .catch(() => setError("Eklenemedi."));
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h2>🚛 Nakliye Yönetimi</h2>

      <form onSubmit={handleSubmit} style={{ marginBottom: "2rem" }}>
        <h3>➕ Yeni Nakliye Kaydı</h3>
        <input
          name="firmaId"
          placeholder="Firma ID"
          value={form.firmaId}
          onChange={handle}
          required
        />
        <input
          name="sahisId"
          placeholder="Şahıs ID"
          value={form.sahisId}
          onChange={handle}
          required
        />
        <input
          name="nakliyeUcreti"
          type="number"
          placeholder="Ücret (₺)"
          value={form.nakliyeUcreti}
          onChange={handle}
          required
        />
        <button type="submit">Kaydet</button>
        {error && <p style={{ color: "red" }}>{error}</p>}
      </form>

      <h3>📋 Nakliye Listesi</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>ID</th>
            <th>Firma</th>
            <th>Şahıs</th>
            <th>Ücret (₺)</th>
          </tr>
        </thead>
        <tbody>
          {nakliyeler.map((n) => (
            <tr key={n.nakliyeId}>
              <td>{n.nakliyeId}</td>
              <td>{n.firmaAdi}</td>
              <td>{n.sahisAdi}</td>
              <td>{n.nakliyeUcreti}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default NakliyeYonetimi;
