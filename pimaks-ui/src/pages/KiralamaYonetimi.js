import React, { useEffect, useState } from "react";
import api from "../api/axios";

const initialFormState = {
  makineId: "",
  firmaId: "",
  baslangicTarihi: "",
  bitisTarihi: "",
  calismaAdresi: "",
  nakliyeFirmasiId: "",
  nakliyeUcreti: "",
  nakliyeciSahisId: "",
};

function KiralamaYonetimi() {
  const [kiralamalar, setKiralamalar] = useState([]);
  const [sahislar, setSahislar] = useState([]);
  const [form, setForm] = useState(initialFormState);

  const [firmalar, setFirmalar] = useState([]);
  const [makineler, setMakineler] = useState([]);
  const [nakliyeFirmalari, setNakliyeFirmalari] = useState([]);

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError("");
      try {
        const [kiralamaRes, firmaRes, makineRes, tedarikciRes, sahisRes] =
          await Promise.all([
            api.get("/kiraliklar"),
            api.get("/firma"),
            api.get("/makine"),
            api.get("/tedarikci"),
            api.get("/sahis"),
          ]);
        setKiralamalar(kiralamaRes.data);
        setFirmalar(firmaRes.data);
        setMakineler(makineRes.data);
        setNakliyeFirmalari(tedarikciRes.data);
        setSahislar(sahisRes.data); // YENİ state'i doldur
      } catch (err) {
        setError("Veriler yüklenemedi.");
        console.error("Veri çekme hatası:", err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleInput = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleCreate = async (e) => {
    e.preventDefault();
    setError("");
    setIsSubmitting(true);

    // --- YENİ: Göndermeden Önce Validasyon ---
    // Gerekli ID alanlarının seçilip seçilmediğini kontrol et.
    if (
      !form.firmaId ||
      !form.makineId ||
      !form.nakliyeFirmasiId ||
      !form.nakliyeciSahisId
    ) {
      setError(
        "Lütfen tüm zorunlu alanları (Firma, Makine, Nakliye Firması, Şoför) seçin."
      );
      setIsSubmitting(false); // Gönderimi durdur
      return; // Fonksiyondan çık
    }

    // Validasyondan geçtiyse, veriyi hazırla. Artık || 0'a gerek yok.
    const dataToSend = {
      ...form,
      firmaId: parseInt(form.firmaId),
      makineId: parseInt(form.makineId),
      nakliyeFirmasiId: parseInt(form.nakliyeFirmasiId),
      nakliyeUcreti: parseFloat(form.nakliyeUcreti) || 0, // Ücret 0 olabilir, bu yüzden || 0 burada kalabilir.
      nakliyeciSahisId: parseInt(form.nakliyeciSahisId),
    };

    try {
      const res = await api.post("/kiraliklar/with-nakliye", dataToSend);
      setKiralamalar([...kiralamalar, res.data]);
      setForm(initialFormState); // Formu temizle
    } catch (err) {
      setError("Kiralama ve Nakliye oluşturulamadı.");
      console.error("Oluşturma hatası:", err.response?.data || err);
    } finally {
      setIsSubmitting(false);
    }
  };

  // KiralamaYonetimi.js içinde

  const handleNakliyeFirmasiChange = async (e) => {
    const nakliyeFirmasiId = e.target.value;

    // 1. Ana formu güncelle
    setForm((prev) => ({
      ...prev,
      nakliyeFirmasiId: nakliyeFirmasiId,
      nakliyeciSahisId: "", // Firma değiştiğinde, önceki şoför seçimini sıfırla
    }));

    // 2. Yeni şoför listesini boşalt
    setSahislar([]);

    // 3. Eğer geçerli bir firma seçilmişse, o firmaya ait şahısları çek
    if (nakliyeFirmasiId) {
      try {
        const res = await api.get(`/sahis?firmaId=${nakliyeFirmasiId}`);
        setSahislar(res.data);
      } catch (err) {
        console.error("Şahıslar çekilemedi:", err);
        setError("Seçilen firmaya ait şoförler yüklenemedi.");
      }
    }
  };

  if (loading) {
    return <div style={{ padding: "2rem" }}>Veriler Yükleniyor...</div>;
  }

  return (
    <div style={{ padding: "2rem" }}>
      <h2>📝 Kiralama Yönetimi</h2>

      <form
        onSubmit={handleCreate}
        style={{
          marginBottom: "2rem",
          display: "flex",
          flexDirection: "column",
          gap: "10px",
        }}
      >
        <h3>➕ Yeni Kiralama ve Nakliye Kaydı</h3>

        <select
          name="firmaId"
          value={form.firmaId}
          onChange={handleInput}
          required
        >
          <option value="">Kiracı Firma Seç</option>
          {firmalar.map((firma) => (
            <option key={firma.firmaId} value={firma.firmaId}>
              {firma.firmaAdi}
            </option>
          ))}
        </select>

        <select
          name="makineId"
          value={form.makineId}
          onChange={handleInput}
          required
        >
          <option value="">Kiralanacak Makine Seç</option>
          {makineler.map((makine) => (
            <option key={makine.makineId} value={makine.makineId}>
              {makine.makineKodu} - {makine.markaAdi}
            </option>
          ))}
        </select>

        {/* YENİ: Nakliye Alanları */}
        <select
          name="nakliyeFirmasiId"
          value={form.nakliyeFirmasiId}
          onChange={handleNakliyeFirmasiChange}
          required
        >
          <option value="">Nakliye Firması Seç</option>
          {nakliyeFirmalari.map((firma) => (
            <option key={firma.tedarikciId} value={firma.firmaId}>
              {firma.firmaAdi}
            </option>
          ))}
        </select>
        <select
          name="nakliyeciSahisId"
          value={form.nakliyeciSahisId}
          onChange={handleInput}
          required
        >
          <option value="">Nakliyeci Şoför Seç</option>
          {sahislar.map((sahis) => (
            <option key={sahis.sahisId} value={sahis.sahisId}>
              {sahis.sahisAdi}
            </option>
          ))}
        </select>
        <input
          name="nakliyeUcreti"
          type="number"
          step="0.01"
          placeholder="Nakliye Ücreti (₺)"
          value={form.nakliyeUcreti}
          onChange={handleInput}
          required
        />

        <input
          name="baslangicTarihi"
          type="date"
          value={form.baslangicTarihi}
          onChange={handleInput}
          required
        />
        <input
          name="bitisTarihi"
          type="date"
          value={form.bitisTarihi}
          onChange={handleInput}
          required
        />
        <input
          name="calismaAdresi"
          placeholder="Çalışma Adresi"
          value={form.calismaAdresi}
          onChange={handleInput}
          required
        />

        <button
          type="submit"
          disabled={isSubmitting}
          style={{ padding: "10px" }}
        >
          {isSubmitting ? "Kaydediliyor..." : "Kiralamayı ve Nakliyeyi Kaydet"}
        </button>
        {error && <p style={{ color: "red" }}>{error}</p>}
      </form>
      <h3>📋 Mevcut Kiralamalar</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>Firma Adı</th>
            <th>Makine Kodu</th>
            <th>Başlangıç Tarihi</th>
            <th>Bitiş Tarihi</th>
            <th>Adres</th>
          </tr>
        </thead>
        <tbody>
          {kiralamalar.map((k) => (
            <tr key={k.kiralamaId}>
              <td>{k.firmaAdi}</td>
              <td>{k.makineKodu}</td>
              <td>{new Date(k.baslangicTarihi).toLocaleDateString("tr-TR")}</td>
              <td>{new Date(k.bitisTarihi).toLocaleDateString("tr-TR")}</td>
              <td>{k.calismaAdresi}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default KiralamaYonetimi;
