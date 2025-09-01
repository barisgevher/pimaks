import React, { useEffect, useState } from "react";
import api from "../api/axios";
import AsyncSelect from "react-select/async"; // YENİ: react-select'in asenkron component'ini import ediyoruz.
import Modal from "../components/Modal";

const initialFirmaState = {
  sahisId: "", // Burası hala ID'yi tutacak
  firmaAdi: "",
  vergiDairesi: "",
  vergiNumarasi: "",
  adresIl: "",
  adresIlce: "",
  adres: "",
  mailAdresi: "",
  telefonNo: "",
  tedarikciMi: false,
};

const initialSahisState = {
  firmaId: "",
  sahisAdi: "",
  sahisTc: "",
  sahisTelefon: "",
  sahisMail: "",
};

function FirmaYonetimi() {
  const [firmalar, setFirmalar] = useState([]);
  const [yeniFirma, setYeniFirma] = useState(initialFirmaState);
  const [hata, setHata] = useState("");
  const [loading, setLoading] = useState(true);
  const [selectedSahis, setSelectedSahis] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [yeniSahis, setYeniSahis] = useState(initialSahisState);
  const [modalHata, setModalHata] = useState("");
  const [isModalLoading, setIsModalLoading] = useState(false);
  const [selectKey, setSelectKey] = useState(0);

  useEffect(() => {
    const fetchFirmalar = async () => {
      setLoading(true);
      try {
        const firmalarRes = await api.get("/firma");
        setFirmalar(firmalarRes.data);
      } catch (err) {
        setHata("Firma listesi alınamadı.");
      } finally {
        setLoading(false);
      }
    };
    fetchFirmalar();
  }, []);

  const handleModalInput = (e) => {
    const { name, value } = e.target;
    setYeniSahis((prev) => ({ ...prev, [name]: value }));
  };

  const handleSahisSubmit = async (e) => {
    e.preventDefault();
    setModalHata("");
    setIsModalLoading(true);

    const dataToSend = {
      ...yeniSahis,
      firmaId: parseInt(yeniSahis.firmaId) || 0,
    };

    try {
      console.log("👤 API'ye gönderilecek Şahıs objesi:", dataToSend);

      const res = await api.post("/sahis", yeniSahis);
      const olusturulanSahis = res.data;
      setIsModalOpen(false);
      setYeniSahis(initialSahisState);
      const newOption = {
        value: olusturulanSahis.sahisId,
        label: olusturulanSahis.sahisAdi,
      };
      handleSahisChange(newOption);
      setSelectKey((prevKey) => prevKey + 1);
    } catch (err) {
      const msg =
        err.response?.data?.message || "Şahıs eklenirken bir hata oluştu.";
      setModalHata(msg);
    } finally {
      setIsModalLoading(false);
    }
  };

  const loadSahisOptions = async (inputValue) => {
    try {
      const response = await api.get(`/sahis?searchTerm=${inputValue}`);
      return response.data.map((s) => ({
        value: s.sahisId,
        label: s.sahisAdi,
      }));
    } catch (error) {
      return [];
    }
  };

  const handleSahisChange = (selectedOption) => {
    setSelectedSahis(selectedOption);
    setYeniFirma((prev) => ({
      ...prev,
      sahisId: selectedOption ? selectedOption.value : "",
    }));
  };

  const handleInput = (e) => {
    const { name, value, type, checked } = e.target;
    const val = type === "checkbox" ? checked : value;
    if (name === "vergiNumarasi" && !/^\d{0,11}$/.test(value)) {
      return;
    }
    setYeniFirma((prev) => ({ ...prev, [name]: val }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setHata("");
    if (!yeniFirma.sahisId) {
      setHata("Lütfen bir şahıs seçin.");
      return;
    }
    const dataToSend = {
      ...yeniFirma,
      sahisId: parseInt(yeniFirma.sahisId) || 0,
    };

    console.log("➡️ API'ye gönderilecek nihai veri:", dataToSend);
    try {
      const res = await api.post("/firma", dataToSend);
      console.log("✅ Yeni firma başarıyla eklendi! API yanıtı:", res.data);
      setFirmalar((prev) => [...prev, res.data]);
      setYeniFirma(initialFirmaState);
      setSelectedSahis(null);
    } catch (err) {
      console.error("❌ Yeni firma eklenemedi! Hata:", err);
      const msg = err.response?.data?.title || "Firma eklenemedi.";
      console.error("❌ Hata Detayları:", err.response?.data);
      setHata(msg);
    }
  };

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div style={{ padding: "2rem" }}>
      <h2>🏢 Firma Yönetimi</h2>
      <form
        onSubmit={handleSubmit}
        style={{
          marginBottom: "2rem",
          display: "flex",
          flexDirection: "column",
          gap: "10px",
        }}
      >
        <h3>➕ Yeni Firma Ekle</h3>

        {/* --- İYİLEŞTİRİLMİŞ KULLANICI ARAYÜZÜ --- */}
        <div style={{ marginBottom: "1rem" }}>
          <label
            style={{
              display: "block",
              marginBottom: "5px",
              fontWeight: "bold",
            }}
          >
            Firma Yetkilisi
          </label>
          <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
            <AsyncSelect
              key={selectKey}
              cacheOptions
              loadOptions={loadSahisOptions}
              defaultOptions
              value={selectedSahis}
              onChange={handleSahisChange}
              placeholder="Şahıs Ara ve Seç..."
              styles={{ container: (base) => ({ ...base, flex: 1 }) }}
            />
            <button
              type="button"
              onClick={() => setIsModalOpen(true)}
              style={{ padding: "8px 12px", cursor: "pointer" }}
            >
              ➕
            </button>
          </div>
        </div>

        {/* Diğer inputlar */}
        <input
          name="firmaAdi"
          placeholder="Firma Adı"
          value={yeniFirma.firmaAdi}
          onChange={handleInput}
          required
        />
        <input
          name="vergiDairesi"
          placeholder="Vergi Dairesi"
          value={yeniFirma.vergiDairesi}
          onChange={handleInput}
        />
        <input
          name="vergiNumarasi"
          placeholder="Vergi No (11 Hane)"
          value={yeniFirma.vergiNumarasi}
          onChange={handleInput}
          required
        />
        <input
          type="email"
          name="mailAdresi"
          placeholder="Mail"
          value={yeniFirma.mailAdresi}
          onChange={handleInput}
          required
        />
        <input
          name="telefonNo"
          placeholder="Telefon"
          value={yeniFirma.telefonNo}
          onChange={handleInput}
          required
        />
        <input
          name="adresIl"
          placeholder="İl"
          value={yeniFirma.adresIl}
          onChange={handleInput}
          required
        />
        <input
          name="adresIlce"
          placeholder="İlçe"
          value={yeniFirma.adresIlce}
          onChange={handleInput}
          required
        />
        <input
          name="adres"
          placeholder="Açık Adres"
          value={yeniFirma.adres}
          onChange={handleInput}
          required
        />

        <label style={{ display: "flex", alignItems: "center", gap: "5px" }}>
          <input
            type="checkbox"
            name="tedarikciMi"
            checked={yeniFirma.tedarikciMi}
            onChange={handleInput}
          />
          Bu firma aynı zamanda bir tedarikçi mi?
        </label>

        <button
          type="submit"
          style={{ padding: "10px", marginTop: "10px", cursor: "pointer" }}
        >
          Kaydet
        </button>
        {hata && <p style={{ color: "red" }}>{hata}</p>}
      </form>

      <h3>📋 Kayıtlı Firmalar</h3>
      <table
        border="1"
        cellPadding="8"
        style={{ width: "100%", borderCollapse: "collapse" }}
      >
        <thead>
          <tr>
            <th>Firma Adı</th>
            <th>Mail</th>
            <th>Telefon</th>
            <th>Konum</th>
            <th>Cari Borç ₺</th>
          </tr>
        </thead>
        <tbody>
          {firmalar.map((f) => (
            <tr key={f.firmaId}>
              <td>{f.firmaAdi}</td>
              <td>{f.mailAdresi}</td>
              <td>{f.telefonNo}</td>
              <td>
                {f.adresIl} / {f.adresIlce}
              </td>
              <td>{f.toplamCariBorc?.toLocaleString("tr-TR")}</td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* YENİ: Şahıs Ekleme Modal'ı */}
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <form onSubmit={handleSahisSubmit}>
          <h3>➕ Yeni Şahıs Ekle</h3>
          <div
            style={{ display: "flex", flexDirection: "column", gap: "10px" }}
          >
            <select
              name="firmaId"
              value={yeniSahis.firmaId}
              onChange={handleModalInput}
              required
            >
              <option value="">Bağlı Olacağı Firmayı Seçin</option>
              {firmalar.map((firma) => (
                <option key={firma.firmaId} value={firma.firmaId}>
                  {firma.firmaAdi}
                </option>
              ))}
            </select>
            <input
              name="sahisAdi"
              placeholder="Adı Soyadı"
              value={yeniSahis.sahisAdi}
              onChange={handleModalInput}
              required
            />
            <input
              name="sahisTc"
              placeholder="TC Kimlik No (İsteğe Bağlı)"
              value={yeniSahis.sahisTc}
              onChange={handleModalInput}
            />
            <input
              name="sahisTelefon"
              placeholder="Telefon Numarası"
              value={yeniSahis.sahisTelefon}
              onChange={handleModalInput}
              required
            />
            <input
              type="email"
              name="sahisMail"
              placeholder="E-posta Adresi"
              value={yeniSahis.sahisMail}
              onChange={handleModalInput}
              required
            />
            <button type="submit" disabled={isModalLoading}>
              {isModalLoading ? "Kaydediliyor..." : "Şahsı Kaydet"}
            </button>
            {modalHata && <p style={{ color: "red" }}>{modalHata}</p>}
          </div>
        </form>
      </Modal>
    </div>
  );
}

export default FirmaYonetimi;
