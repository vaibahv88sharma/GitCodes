export class Alumno {

    public id: number;
    public nombre: string;
    public apellidos: string;
    public nota: number;

    constructor();
    constructor(nombre: string, apellidos: string, nota: number, id?: number) {

        this.id = id;
        this.nombre = nombre;
        this.apellidos = apellidos;
        this.nota = nota;

    }
}