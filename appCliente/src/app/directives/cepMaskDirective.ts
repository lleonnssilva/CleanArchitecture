import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appCepMask]' // Seletor para aplicar a diretiva no HTML
})
export class CepMaskDirective {

  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event']) // Escuta o evento de input
  onInput(event: Event): void {
    const input = this.el.nativeElement;
    let value = input.value.replace(/\D/g, ''); // Remove qualquer caractere não numérico

    // Aplica a máscara no formato XXXXX-XXX
    if (value.length <= 5) {
      value = value.replace(/(\d{2})(\d{0,3})/, '$1-$2'); // Máscara para até 5 dígitos
    } else {
      value = value.replace(/(\d{2})(\d{3})(\d{0,3})/, '$1-$2-$3'); // Máscara para 8 ou 9 dígitos
    }

    input.value = value; // Atualiza o valor do input
  }
}
