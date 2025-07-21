import React, { useEffect, useState } from "react";
import api from "../api/axios";

function CariTahsilatYonetimi() {
  const [cariler, setCariler] = useState([]);
  const [form, setForm] = useState({
    firmaId: "",
    tahsilatMiktari: "",
    odemeTipi: "",
    kdvorani: "",
  });
  const [error, setError] = useState("");

  useEffect(() => {
    api
      .get("/cari")
      .then((res) => setCariler(res.data))
      .catch(() => setError("Cari verisi yüklenemedi."));
  }, []);

  const handle = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleTahsilat = (e) => {
    e.preventDefault();
    api
      .post("/cari/tahsilat", {
        ...form,
        tahsilatMiktari: parseFloat(form.tahsilatMiktari),
        odemeTipi: parseInt(form.odemeTipi),
        kdvorani: parseInt(form.kdvorani),
        tahsilatTarihi: new Date().toISOString(),
      })
      .then((res) => {
        setError("");
        return api.get("/cari");
      })
      .then((res) => {
        setCariler(res.data);
        setForm({
          firmaId: "",
          tahsilatMiktari: "",
          odemeTipi: "",
          kdvorani: "",
        });
      })
      .catch(() => setError("Tahsilat eklenemedi."));
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h2>💰 Cari Hesap & Tahsilat</h2>

      <form onSubmit={handleTahsilat} style={{ marginBottom: "2rem" }}>
        <h3>➕ Tahsilat Girişi</h3>
        <select name="firmaId" value={form.firmaId} onChange={handle} required>
          <option value="">Firma Seç</option>
          {cariler.map((c) => (
            <option key={c.firmaId} value={c.firmaId}>
              {c.firmaAdi}
            </option>
          ))}
        </select>
        <input
          name="tahsilatMiktari"
          type="number"
          placeholder="Tahsilat Miktarı"
          value={form.tahsilatMiktari}
          onChange={handle}
          required
        />
        <select
          name="odemeTipi"
          value={form.odemeTipi}
          onChange={handle}
          required
        >
          <option value="">Ödeme Tipi</option>
          <option value="0">Nakit</option>
          <option value="1">Kredi Kartı</option>
          <option value="2">Transfer</option>
        </select>
        <input
          name="kdvorani"
          type="number"
          placeholder="KDV %"
          value={form.kdvorani}
          onChange={handle}
          required
        />
        <button type="submit">Tahsilat Yap</button>
        {error && <p style={{ color: "red" }}>{error}</p>}
      </form>

      <h3>📋 Cari Listesi</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>Firma Adı</th>
            <th>Toplam Borç (₺)</th>
          </tr>
        </thead>
        <tbody>
          {cariler.map((c) => (
            <tr key={c.firmaId}>
              <td>{c.firmaAdi}</td>
              <td>{c.toplamBorc.toLocaleString("tr-TR")}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CariTahsilatYonetimi;
