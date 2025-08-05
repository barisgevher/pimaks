import React, { useEffect, useState } from "react";
import api from "../api/axios";

function IstatistikSayfasi() {
  // STATE'LER
  const [makineler, setMakineler] = useState([]); // Makine seçimi dropdown'ı için
  const [genelIstatistikler, setGenelIstatistikler] = useState([]); // Genel performans tablosu için
  const [params, setParams] = useState({
    // Detaylı analiz formu için parametreler
    makineId: "",
    baslangicTarihi: "",
    bitisTarihi: "",
  });
  const [sonuc, setSonuc] = useState(null); // Detaylı analiz sonucu için
  const [loading, setLoading] = useState(true); // Sayfa yüklenme durumu
  const [isCalculating, setIsCalculating] = useState(false); // Hesaplama butonu yüklenme durumu
  const [error, setError] = useState("");

  // VERİ ÇEKME
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError("");
      try {
        const [makinelerRes, genelIstatistiklerRes] = await Promise.all([
          api.get("/makine"),
          api.get("/istatistik/makine-genel"),
        ]);
        setMakineler(makinelerRes.data);
        setGenelIstatistikler(genelIstatistiklerRes.data);
      } catch (err) {
        setError("Gerekli veriler yüklenemedi.");
        console.error("Veri çekme hatası:", err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  // HANDLER'LAR
  const handleInput = (e) => {
    const { name, value } = e.target;
    setParams((prev) => ({ ...prev, [name]: value }));
  };

  const handleHesapla = async (e) => {
    e.preventDefault();
    if (!params.makineId || !params.baslangicTarihi || !params.bitisTarihi) {
      setError("Lütfen bir makine ve tarih aralığı seçin.");
      return;
    }
    setIsCalculating(true);
    setError("");
    setSonuc(null);

    const dataToSend = {
      ...params,
      makineId: parseInt(params.makineId, 10),
    };

    try {
      const res = await api.post("/istatistik/makine", dataToSend);
      setSonuc(res.data);
    } catch (err) {
      setError("İstatistik hesaplanamadı.");
      console.error("Hesaplama hatası:", err.response?.data || err);
    } finally {
      setIsCalculating(false);
    }
  };

  // RENDER
  if (loading) {
    return <div style={{ padding: "2rem" }}>Veriler Yükleniyor...</div>;
  }

  return (
    <div style={{ padding: "2rem" }}>
      <h2>📈 Makine Performans Analizi</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}

      <form
        onSubmit={handleHesapla}
        style={{
          display: "flex",
          gap: "1rem",
          alignItems: "center",
          marginBottom: "2rem",
          padding: "1rem",
          border: "1px solid #ddd",
          borderRadius: "8px",
        }}
      >
        <select
          name="makineId"
          value={params.makineId}
          onChange={handleInput}
          required
        >
          <option value="">Makine Seç</option>
          {makineler.map((m) => (
            <option key={m.makineId} value={m.makineId}>
              {m.makineKodu} - {m.markaAdi}
            </option>
          ))}
        </select>
        <input
          name="baslangicTarihi"
          type="date"
          value={params.baslangicTarihi}
          onChange={handleInput}
          required
        />
        <span>-</span>
        <input
          name="bitisTarihi"
          type="date"
          value={params.bitisTarihi}
          onChange={handleInput}
          required
        />
        <button type="submit" disabled={isCalculating}>
          {isCalculating ? "Hesaplanıyor..." : "Hesapla"}
        </button>
      </form>

      {sonuc && (
        <div
          style={{
            border: "1px solid #ccc",
            padding: "1rem",
            borderRadius: "8px",
            marginBottom: "2rem",
            backgroundColor: "#f9f9f9",
          }}
        >
          <h3>Analiz Sonucu</h3>
          <p>
            <strong>Kiralama Sayısı:</strong> {sonuc.kiralamaSayisi} adet
          </p>
          <p>
            <strong>Seçilen Aralıkta Toplam Çalıştığı Gün:</strong>{" "}
            {sonuc.toplamCalisilanGun} gün
          </p>
          <p>
            <strong>Seçilen Aralıkta Toplam Getirisi:</strong>
            {sonuc.toplamGetiri.toLocaleString("tr-TR", {
              style: "currency",
              currency: "TRY",
            })}
          </p>
        </div>
      )}

      <hr style={{ margin: "2rem 0" }} />

      <h3>📊 Tüm Makineler İçin Genel Performans</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>Makine Kodu</th>
            <th>Marka</th>
            <th>Toplam Kiralama Sayısı</th>
            <th>Toplam Getiri (₺)</th>
          </tr>
        </thead>
        <tbody>
          {genelIstatistikler.map((stat) => (
            <tr key={stat.makineId}>
              <td>{stat.makineKodu}</td>
              <td>{stat.markaAdi}</td>
              <td>{stat.toplamKiralamaSayisi}</td>
              <td style={{ textAlign: "right", fontWeight: "bold" }}>
                {stat.toplamGetiri.toLocaleString("tr-TR", {
                  style: "currency",
                  currency: "TRY",
                })}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default IstatistikSayfasi;
