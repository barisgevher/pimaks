import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import AnaSayfa from "./pages/AnaSayfa";
import TestApi from "./pages/TestApi";
import FirmaYonetimi from "./pages/FirmaYonetimi";
import KiralamaYonetimi from "./pages/KiralamaYonetimi";
import NakliyeYonetimi from "./pages/NakliyeYonetimi";
import CariTahsilatYonetimi from "./pages/CariTahsilatYonetimi";
import MakineYonetimi from "./pages/MakineYonetimi";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<AnaSayfa />} />
        <Route path="/firmalar" element={<FirmaYonetimi />} />
        <Route path="/makineler" element={<MakineYonetimi />} />
        <Route path="/kiraliklar" element={<KiralamaYonetimi />} />
        <Route path="/nakliye" element={<NakliyeYonetimi />} />
        <Route path="/finans" element={<CariTahsilatYonetimi />} />
        <Route path="/" element={<TestApi />} />
      </Routes>
    </Router>
  );
}

export default App;
