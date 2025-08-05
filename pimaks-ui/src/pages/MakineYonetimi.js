import React, { useEffect, useState } from "react";
import api from "../api/axios";

// Formu sıfırlamak için kullanılacak başlangıç state'i
const initialMakineState = {
  makineKodu: "",
  tedarikciId: "",
  markaId: "", // 'marka' yerine 'markaId'
  model: "",
  seriNo: "",
  imalatYili: "",
  kaldirmaKapasitesiKg: "",
  calismaYuksekligi: "",
  tipId: "", // 'makineTipi' yerine 'tipId'
  yakitId: "", // 'yakit' yerine 'yakitId'
  kiradaMi: false,
  birimFiyat: "",
  calismaYuzdesi: "",
};

function MakineYonetimi() {
  // STATE'LER
  const [makineler, setMakineler] = useState([]);
  const [tedarikciler, setTedarikciler] = useState([]);
  const [markalar, setMarkalar] = useState([]);
  const [yakitTipleri, setYakitTipleri] = useState([]);
  const [makineTipleri, setMakineTipleri] = useState([]);
  const [yeniMakine, setYeniMakine] = useState(initialMakineState);
  const [hata, setHata] = useState("");
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);

  // VERİ ÇEKME
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setHata("");
      try {
        const [
          makinelerRes,
          tedarikcilerRes,
          markalarRes,
          yakitTipleriRes,
          makineTipleriRes,
        ] = await Promise.all([
          api.get("/makine"),
          api.get("/tedarikci"),
          api.get("/marka"),
          api.get("/yakit"),
          api.get("/makineTipi"),
        ]);

        setMakineler(makinelerRes.data);
        setTedarikciler(tedarikcilerRes.data);
        setMarkalar(markalarRes.data);
        setYakitTipleri(yakitTipleriRes.data);
        setMakineTipleri(makineTipleriRes.data);
      } catch (err) {
        setHata("Gerekli veriler yüklenemedi. Lütfen sayfayı yenileyin.");
        console.error("Veri çekme hatası:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  // HANDLER'LAR
  const handleInput = (e) => {
    const { name, value, type, checked } = e.target;
    const val = type === "checkbox" ? checked : value;
    setYeniMakine((prev) => ({ ...prev, [name]: val }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setHata("");
    setIsSubmitting(true);

    const dataToSend = {
      ...yeniMakine,
      tedarikciId: parseInt(yeniMakine.tedarikciId) || 0,
      markaId: parseInt(yeniMakine.markaId) || 0,
      imalatYili: parseInt(yeniMakine.imalatYili) || 0,
      kaldirmaKapasitesiKg: yeniMakine.kaldirmaKapasitesiKg
        ? parseInt(yeniMakine.kaldirmaKapasitesiKg)
        : null,
      calismaYuksekligi: yeniMakine.calismaYuksekligi
        ? parseInt(yeniMakine.calismaYuksekligi)
        : null,
      tipId: parseInt(yeniMakine.tipId) || 0,
      yakitId: parseInt(yeniMakine.yakitId) || 0,
      birimFiyat: parseInt(yeniMakine.birimFiyat) || 0,
    };

    console.log("➡️ API'ye Gönderilecek Veri:", dataToSend);

    try {
      const res = await api.post("/makine", dataToSend);
      // Gelen yanıtın da düzleştirilmiş veriler içerdiğini varsayıyoruz
      setMakineler((prev) => [...prev, res.data]);
      setYeniMakine(initialMakineState);
    } catch (err) {
      setHata("Makine eklenemedi. Lütfen verileri kontrol edin.");
      console.error("Makine ekleme hatası:", err.response?.data || err);
    } finally {
      setIsSubmitting(false);
    }
  };

  // RENDER
  if (loading) {
    return <div style={{ padding: "2rem" }}>Veriler Yükleniyor...</div>;
  }

  return (
    <div style={{ padding: "2rem" }}>
      <h2>🔧 Makine Yönetimi</h2>

      <form
        onSubmit={handleSubmit}
        style={{
          marginBottom: "2rem",
          display: "flex",
          flexDirection: "column",
          gap: "10px",
        }}
      >
        <h3>➕ Yeni Makine Ekle</h3>

        <input
          name="makineKodu"
          placeholder="Makine Kodu"
          value={yeniMakine.makineKodu}
          onChange={handleInput}
          required
        />
        <input
          name="model"
          placeholder="Model"
          value={yeniMakine.model}
          onChange={handleInput}
        />
        <input
          name="seriNo"
          placeholder="Seri No"
          value={yeniMakine.seriNo}
          onChange={handleInput}
        />

        <select
          name="tedarikciId"
          value={yeniMakine.tedarikciId}
          onChange={handleInput}
          required
        >
          <option value="">Tedarikçi Seç</option>
          {tedarikciler.map((t) => (
            <option key={t.tedarikciId} value={t.tedarikciId}>
              {t.firmaAdi}
            </option>
          ))}
        </select>

        <select
          name="markaId"
          value={yeniMakine.markaId}
          onChange={handleInput}
          required
        >
          <option value="">Marka Seç</option>
          {markalar.map((marka) => (
            <option key={marka.markaId} value={marka.markaId}>
              {marka.marka1}
            </option>
          ))}
        </select>

        <select
          name="tipId"
          value={yeniMakine.tipId}
          onChange={handleInput}
          required
        >
          <option value="">Makine Tipi Seç</option>
          {makineTipleri.map((tip) => (
            <option key={tip.tipId} value={tip.tipId}>
              {tip.makineTipi1}
            </option>
          ))}
        </select>

        <select
          name="yakitId"
          value={yeniMakine.yakitId}
          onChange={handleInput}
          required
        >
          <option value="">Yakıt Tipi Seç</option>
          {yakitTipleri.map((yakit) => (
            <option key={yakit.yakitId} value={yakit.yakitId}>
              {yakit.yakit1}
            </option>
          ))}
        </select>

        <input
          name="imalatYili"
          type="number"
          placeholder="İmalat Yılı"
          value={yeniMakine.imalatYili}
          onChange={handleInput}
        />
        <input
          name="kaldirmaKapasitesiKg"
          type="number"
          placeholder="Kaldırma Kapasitesi (kg)"
          value={yeniMakine.kaldirmaKapasitesiKg}
          onChange={handleInput}
          required
        />
        <input
          name="calismaYuksekligi"
          type="number"
          placeholder="Çalışma Yüksekliği (m)"
          value={yeniMakine.calismaYuksekligi}
          onChange={handleInput}
        />
        <input
          name="birimFiyat"
          type="number"
          placeholder="Birim Fiyatı (₺)"
          value={yeniMakine.birimFiyat}
          onChange={handleInput}
        />
        <input
          name="calismaYuzdesi"
          placeholder="Çalışma Yüzdesi (%)"
          value={yeniMakine.calismaYuzdesi}
          onChange={handleInput}
        />

        <label style={{ display: "flex", alignItems: "center", gap: "5px" }}>
          <input
            type="checkbox"
            name="kiradaMi"
            checked={yeniMakine.kiradaMi}
            onChange={handleInput}
          />
          Şu Anda Kirada mı?
        </label>

        <button
          type="submit"
          disabled={isSubmitting}
          style={{ padding: "10px", marginTop: "10px" }}
        >
          {isSubmitting ? "Kaydediliyor..." : "Kaydet"}
        </button>

        {hata && <p style={{ color: "red" }}>{hata}</p>}
      </form>

      <h3>📋 Kayıtlı Makineler</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>Kodu</th>
            <th>Marka</th>
            <th>Model</th>
            <th>İmalat Yılı</th>
            <th>Kapasite (kg)</th>
            <th>Tip</th>
            <th>Yakıt</th>
            <th>Birim Fiyat (₺)</th>
          </tr>
        </thead>
        <tbody>
          {makineler.map((m) => (
            <tr
              key={m.makineId}
              style={{ backgroundColor: m.kiradaMi ? "#e8f5e9" : "#ffebee" }}
            >
              <td>{m.makineKodu}</td>
              <td>{m.markaAdi}</td>
              <td>{m.model}</td>
              <td>{m.imalatYili}</td>
              <td>{m.kaldirmaKapasitesiKg}</td>
              <td>{m.tipAdi}</td>
              <td>{m.yakitAdi}</td>
              <td>{m.birimFiyat}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MakineYonetimi;
