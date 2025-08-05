import React, { useEffect, useState } from "react";
import api from "../api/axios";
import { Link } from "react-router-dom";

function AnaSayfa() {
  const [stats, setStats] = useState({
    firmaIsimleri: [],
    firmaSayisi: 0,
    makineSayisi: 0,
    aktifKiralama: 0,
    toplamBorc: 0,
  });

  useEffect(() => {
    api
      .get("/AnaSayfa") // Backend'de endpoint olacak
      .then((res) => setStats(res.data))
      .catch((err) => console.error("AnaSayfa verisi alınamadı", err));
  }, []);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>📊 Kontrol Paneli</h1>

      <div
        className="cards"
        style={{ display: "flex", gap: "2rem", marginTop: "1rem" }}
      >
        <Card title="Toplam Firma" value={stats.firmaSayisi} />
        <Card title="Toplam Makine" value={stats.makineSayisi} />
        <Card title="Aktif Kiralama" value={stats.aktifKiralama} />
        <Card title="Firma Adları" value={stats.firmaIsimleri.join(", ")} />
        <Card
          title="Toplam Borç (₺)"
          value={stats.toplamBorc.toLocaleString("tr-TR")}
        />
      </div>

      <div style={{ marginTop: "2rem" }}>
        <h2>⚡ Hızlı Erişim</h2>
        <ul>
          <li>
            <Link to="/firmalar">📁 Firma Yönetimi</Link>
          </li>
          <li>
            <Link to="/makineler">🔧 Makine Yönetimi</Link>
          </li>
          <li>
            <Link to="/kiraliklar">📝 Yeni Kiralama</Link>
          </li>
          <li>
            <Link to="/nakliye"> 🤖 Nakliyeler</Link>
          </li>
          <li>
            <Link to="/finans">💰 Cari Hesap ve Tahsilat</Link>
          </li>
          <li>
            <Link to="/istatistikler">📊 İstatistikler</Link>
          </li>
        </ul>
      </div>
    </div>
  );
}

function Card({ title, value }) {
  return (
    <div
      style={{
        backgroundColor: "#f1f1f1",
        padding: "1rem",
        borderRadius: "8px",
        width: "200px",
        textAlign: "center",
      }}
    >
      <h3>{title}</h3>
      <p style={{ fontSize: "24px", fontWeight: "bold" }}>{value}</p>
    </div>
  );
}

export default AnaSayfa;
