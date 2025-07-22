import { AbstractControl, ValidationErrors } from '@angular/forms';

// Validador de CEP (Formato: XXXXX-XXX)
export function cepValidator(control: AbstractControl): ValidationErrors | null {
  const cepPattern = /^[0-9]{5}-[0-9]{3}$/;  // Ex: 12345-678
  const valid = cepPattern.test(control.value);
  return valid ? null : { invalidCep: true };
}