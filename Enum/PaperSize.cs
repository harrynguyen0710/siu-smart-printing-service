using System.ComponentModel.DataAnnotations;

namespace siu_smart_printing_service.Enum
{
    public enum PaperSize
    {
        [Display(Name = "A4 (594 x 841mm)")]
        A1,
        [Display(Name = "A4 (420 x 594mm)")]
        A2,
        [Display(Name = "A4 (279 x 420mm)")]
        A3,
        [Display(Name = "A4 (210 x 297mm)")]
        A4,
        [Display(Name = "A4 (148 x 210mm)")]
        A5,
    }
}
