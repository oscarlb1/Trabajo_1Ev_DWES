// app.js - Versión Final Corregida (para arrays JSON directos)
// ===================================================================

// API Configuration
const API_BASE_URL = 'https://localhost:7273/api';

let currentEditingId = null;
let currentEditingType = null;

// ===================================================================
// DIAGNÓSTICO: Comprobar que las librerías se cargaron correctamente
// ===================================================================
function checkDependencies() {
    if (typeof axios === 'undefined') {
        console.error("⛔ ERROR CRÍTICO: 'axios' no está definido. Revisa la etiqueta <script> en tu HTML.");
        const dashboardCards = document.getElementById('dashboardCards');
        if (dashboardCards) {
             dashboardCards.innerHTML = '<div class="alert alert-danger" role="alert"><strong>Error de carga:</strong> La librería Axios no está disponible. Revisa tu consola y tu HTML.</div>';
        }
        return false;
    }
    if (typeof Swal === 'undefined') {
        console.warn("⚠️ Advertencia: 'Swal' (SweetAlert2) no está definido. Las alertas de éxito/error no funcionarán.");
        window.Swal = { fire: (options) => console.log('Alerta simulada (Swal no disponible):', options) };
    }
    return true;
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    console.log("🟢 1. DOMContentLoaded disparado. Iniciando script.");
    
    if (!checkDependencies()) {
        console.error("❌ Deteniendo inicialización debido a que Axios no se cargó.");
        return; 
    }

    try {
        console.log("🟢 2. Mostrando sección inicial ('dashboard').");
        showSection('dashboard');
    } catch(e) {
        console.error("❌ Error durante la inicialización DOM:", e);
    }
});

// Show/Hide sections
function showSection(sectionId) {
    const sections = document.querySelectorAll('.section-content');
    sections.forEach(section => section.style.display = 'none');
    
    const targetSection = document.getElementById(sectionId);
    if (targetSection) {
        targetSection.style.display = 'block';
        
        // Load data based on section
        if (sectionId === 'dashboard') {
            loadDashboard();
        } else if (sectionId === 'cursos') {
            loadCursos();
        } else if (sectionId === 'usuarios') {
            loadUsuarios();
        } else if (sectionId === 'profesores') {
            loadProfesores();
        } else if (sectionId === 'materias') {
            loadMaterias();
        } else if (sectionId === 'inscripciones') {
            loadInscripciones();
        } else if (sectionId === 'lecciones') {
            loadLecciones();
        }
    }
}

// Dashboard
async function loadDashboard() {
    console.log(`🟡 3. Iniciando loadDashboard. URL Base: ${API_BASE_URL}`);
    try {
        console.log("🟡 4. Enviando 6 solicitudes axios simultáneamente (revisa pestaña Network)...");
        
        // **IMPORTANTE:** Aumentamos PageSize para contar todos los registros en el dashboard.
        const [cursos, usuarios, profesores, materias, inscripciones, lecciones] = await Promise.all([
            axios.get(`${API_BASE_URL}/Curso?PageNumber=1&PageSize=100`),
            axios.get(`${API_BASE_URL}/Usuario?PageNumber=1&PageSize=100`),
            axios.get(`${API_BASE_URL}/Profesor?PageNumber=1&PageSize=100`),
            axios.get(`${API_BASE_URL}/Materia?PageNumber=1&PageSize=100`),
            axios.get(`${API_BASE_URL}/Inscripcion?PageNumber=1&PageSize=100`),
            axios.get(`${API_BASE_URL}/Leccion?PageNumber=1&PageSize=100`)
        ]);
        
        console.log("🟢 5. Respuestas de API recibidas con éxito. Contando registros...");

        // CORRECCIÓN FINAL: Usamos .data.length ya que la API devuelve un array directo.
        const stats = [
            { title: 'Total Cursos', value: cursos.data.length || 0, icon: 'bi-book', color: 'blue' },
            { title: 'Total Usuarios', value: usuarios.data.length || 0, icon: 'bi-people', color: 'green' },
            { title: 'Total Profesores', value: profesores.data.length || 0, icon: 'bi-person-badge', color: 'orange' },
            { title: 'Total Materias', value: materias.data.length || 0, icon: 'bi-collection', color: 'red' },
            { title: 'Total Inscripciones', value: inscripciones.data.length || 0, icon: 'bi-clipboard-check', color: 'purple' },
            { title: 'Total Lecciones', value: lecciones.data.length || 0, icon: 'bi-file-text', color: 'cyan' }
        ];

        const dashboardCards = document.getElementById('dashboardCards');
        if (dashboardCards) {
             dashboardCards.innerHTML = stats.map(stat => `
                <div class="col-md-6 col-lg-4">
                    <div class="dashboard-card ${stat.color}">
                        <h5><i class="bi ${stat.icon}"></i> ${stat.title}</h5>
                        <div class="value">${stat.value}</div>
                    </div>
                </div>
            `).join('');
        } else {
             console.error("❌ Elemento dashboardCards no encontrado en el DOM.");
        }
        
    } catch (error) {
        console.error("❌ Error CRÍTICO en loadDashboard:", error);
        showError('Error al cargar el dashboard', error);
    }
}

// CURSOS
async function loadCursos() {
    console.log("🟡 Iniciando loadCursos...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Curso?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Cursos cargados: ${data.length} registros.`);
        
        const html = data.map(curso => `
            <tr>
                <td>${curso.id}</td>
                <td>${curso.titulo}</td>
                <td>$${curso.costo.toFixed(2)}</td>
                <td>${curso.horas}h</td>
                <td>${curso.profesorId}</td>
                <td>${curso.materiaId}</td>
                <td><span class="badge ${curso.publicado ? 'bg-success' : 'bg-warning'}">${curso.publicado ? 'Sí' : 'No'}</span></td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('curso', ${curso.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Curso', ${curso.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('cursosList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadCursos:", error);
        showError('Error al cargar cursos', error);
    }
}

// USUARIOS
async function loadUsuarios() {
    console.log("🟡 Iniciando loadUsuarios...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Usuario?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Usuarios cargados: ${data.length} registros.`);
        
        const html = data.map(usuario => `
            <tr>
                <td>${usuario.id}</td>
                <td>${usuario.nombre}</td>
                <td>${usuario.email}</td>
                <td>${usuario.creditos}</td>
                <td>${usuario.cursos}</td>
                <td><span class="badge ${usuario.premium ? 'bg-success' : 'bg-secondary'}">${usuario.premium ? 'Sí' : 'No'}</span></td>
                <td>${new Date(usuario.registro).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('usuario', ${usuario.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Usuario', ${usuario.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('usuariosList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadUsuarios:", error);
        showError('Error al cargar usuarios', error);
    }
}

// PROFESORES
async function loadProfesores() {
    console.log("🟡 Iniciando loadProfesores...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Profesor?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Profesores cargados: ${data.length} registros.`);
        
        const html = data.map(profesor => `
            <tr>
                <td>${profesor.id}</td>
                <td>${profesor.nombre}</td>
                <td>${profesor.especialidad}</td>
                <td>${profesor.email}</td>
                <td>${profesor.experiencia} años</td>
                <td>$${profesor.salario.toFixed(2)}</td>
                <td><span class="badge ${profesor.certificado ? 'bg-success' : 'bg-warning'}">${profesor.certificado ? 'Sí' : 'No'}</span></td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('profesor', ${profesor.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Profesor', ${profesor.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('profesoresList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadProfesores:", error);
        showError('Error al cargar profesores', error);
    }
}

// MATERIAS
async function loadMaterias() {
    console.log("🟡 Iniciando loadMaterias...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Materia?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Materias cargadas: ${data.length} registros.`);
        
        const html = data.map(materia => `
            <tr>
                <td>${materia.id}</td>
                <td>${materia.nombre}</td>
                <td>${materia.detalle}</td>
                <td><span class="badge bg-info">${materia.nivel}</span></td>
                <td>${materia.cantidad}</td>
                <td><span class="badge ${materia.obligatoria ? 'bg-success' : 'bg-secondary'}">${materia.obligatoria ? 'Sí' : 'No'}</span></td>
                <td>${new Date(materia.creacion).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('materia', ${materia.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Materia', ${materia.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('materiasList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadMaterias:", error);
        showError('Error al cargar materias', error);
    }
}

// INSCRIPCIONES
async function loadInscripciones() {
    console.log("🟡 Iniciando loadInscripciones...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Inscripcion?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Inscripciones cargadas: ${data.length} registros.`);
        
        const html = data.map(inscripcion => `
            <tr>
                <td>${inscripcion.id}</td>
                <td>${inscripcion.usuarioId}</td>
                <td>${inscripcion.cursoId}</td>
                <td><div class="progress" style="height: 20px;"><div class="progress-bar" style="width: ${inscripcion.progreso}%">${inscripcion.progreso}%</div></div></td>
                <td>${inscripcion.nota.toFixed(2)}</td>
                <td><span class="badge ${inscripcion.activa ? 'bg-success' : 'bg-danger'}">${inscripcion.activa ? 'Activa' : 'Inactiva'}</span></td>
                <td>${new Date(inscripcion.inscripcionFecha).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('inscripcion', ${inscripcion.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Inscripcion', ${inscripcion.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('inscripcionesList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadInscripciones:", error);
        showError('Error al cargar inscripciones', error);
    }
}

// LECCIONES
async function loadLecciones() {
    console.log("🟡 Iniciando loadLecciones...");
    try {
        const response = await axios.get(`${API_BASE_URL}/Leccion?PageNumber=1&PageSize=100`);
        const data = response.data || []; // <--- CORRECCIÓN APLICADA AQUÍ
        console.log(`🟢 Lecciones cargadas: ${data.length} registros.`);
        
        const html = data.map(leccion => `
            <tr>
                <td>${leccion.id}</td>
                <td>${leccion.titulo}</td>
                <td><span class="badge bg-primary">${leccion.tipo}</span></td>
                <td>${leccion.minutos} min</td>
                <td>${leccion.cursoId}</td>
                <td><span class="badge ${leccion.examen ? 'bg-danger' : 'bg-success'}">${leccion.examen ? 'Sí' : 'No'}</span></td>
                <td>${new Date(leccion.publicacion).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="editRecord('leccion', ${leccion.id})"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRecord('Leccion', ${leccion.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>
        `).join('');
        
        document.getElementById('leccionesList').innerHTML = html;
    } catch (error) {
        console.error("❌ Error en loadLecciones:", error);
        showError('Error al cargar lecciones', error);
    }
}

// Open Modal for Creating/Editing
async function openModal(type) {
    currentEditingType = type;
    currentEditingId = null;
    
    document.getElementById('modalTitle').textContent = `Nuevo ${getTypeName(type)}`;
    document.getElementById('modalBody').innerHTML = getFormHTML(type);
    
    // new bootstrap.Modal requiere que el objeto bootstrap esté cargado
    if (typeof bootstrap !== 'undefined' && bootstrap.Modal) {
        new bootstrap.Modal(document.getElementById('formModal')).show();
    } else {
        console.error("❌ Bootstrap Modal no está disponible.");
    }
}

// Edit Record
async function editRecord(type, id) {
    try {
        currentEditingType = type;
        currentEditingId = id;
        
        const endpoint = getEndpoint(type);
        const response = await axios.get(`${API_BASE_URL}/${endpoint}/${id}`);
        const data = response.data;
        
        document.getElementById('modalTitle').textContent = `Editar ${getTypeName(type)}`;
        document.getElementById('modalBody').innerHTML = getFormHTML(type, data);
        
        if (typeof bootstrap !== 'undefined' && bootstrap.Modal) {
            new bootstrap.Modal(document.getElementById('formModal')).show();
        } else {
            console.error("❌ Bootstrap Modal no está disponible.");
        }
    } catch (error) {
        showError('Error al cargar registro', error);
    }
}

// Save Record
async function saveRecord() {
    try {
        const formData = getFormData(currentEditingType);
        const endpoint = getEndpoint(currentEditingType);
        
        if (currentEditingId) {
            await axios.put(`${API_BASE_URL}/${endpoint}/${currentEditingId}`, formData);
            showSuccess('Registro actualizado correctamente');
        } else {
            await axios.post(`${API_BASE_URL}/${endpoint}`, formData);
            showSuccess('Registro creado correctamente');
        }
        
        if (typeof bootstrap !== 'undefined' && bootstrap.Modal) {
            bootstrap.Modal.getInstance(document.getElementById('formModal')).hide();
        }
        showSection(currentEditingType + 's');
    } catch (error) {
        showError('Error al guardar', error);
    }
}

// Delete Record
async function deleteRecord(endpoint, id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción no se puede deshacer',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                await axios.delete(`${API_BASE_URL}/${endpoint}/${id}`);
                showSuccess('Registro eliminado correctamente');
                showSection(endpoint.toLowerCase() + 's');
            } catch (error) {
                showError('Error al eliminar', error);
            }
        }
    });
}

// Helper Functions
function getEndpoint(type) {
    const endpoints = {
        'curso': 'Curso',
        'usuario': 'Usuario',
        'profesor': 'Profesor',
        'materia': 'Materia',
        'inscripcion': 'Inscripcion',
        'leccion': 'Leccion'
    };
    return endpoints[type] || type;
}

function getTypeName(type) {
    const names = {
        'curso': 'Curso',
        'usuario': 'Usuario',
        'profesor': 'Profesor',
        'materia': 'Materia',
        'inscripcion': 'Inscripción',
        'leccion': 'Lección'
    };
    return names[type] || type;
}

function getFormHTML(type, data = null) {
    const forms = {
        'curso': `
            <div class="form-group mb-3">
                <label class="form-label">Título</label>
                <input type="text" class="form-control" id="titulo" value="${data?.titulo || ''}" placeholder="Título del curso">
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Detalle</label>
                <textarea class="form-control" id="detalle" placeholder="Detalles del curso">${data?.detalle || ''}</textarea>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Costo</label>
                    <input type="number" class="form-control" id="costo" value="${data?.costo || ''}" step="0.01" placeholder="0.00">
                </div>
                <div class="col-md-6">
                    <label class="form-label">Horas</label>
                    <input type="number" class="form-control" id="horas" value="${data?.horas || ''}" placeholder="40">
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Profesor ID</label>
                    <input type="number" class="form-control" id="profesorId" value="${data?.profesorId || ''}" placeholder="1">
                </div>
                <div class="col-md-6">
                    <label class="form-label">Materia ID</label>
                    <input type="number" class="form-control" id="materiaId" value="${data?.materiaId || ''}" placeholder="1">
                </div>
            </div>
            <div class="form-group mb-3">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="publicado" ${data?.publicado ? 'checked' : ''}>
                    <label class="form-check-label">Publicado</label>
                </div>
            </div>
            <div class="form-group">
                <label class="form-label">Creación</label>
                <input type="date" class="form-control" id="creacion" value="${data?.creacion ? data.creacion.split('T')[0] : ''}">
            </div>
        `,
        'usuario': `
            <div class="form-group mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" value="${data?.nombre || ''}" placeholder="Nombre completo">
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Email</label>
                <input type="email" class="form-control" id="email" value="${data?.email || ''}" placeholder="email@example.com">
            </div>
            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="form-label">Créditos</label>
                    <input type="number" class="form-control" id="creditos" value="${data?.creditos || '0'}">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Cursos</label>
                    <input type="number" class="form-control" id="cursos" value="${data?.cursos || '0'}">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Registro</label>
                    <input type="date" class="form-control" id="registro" value="${data?.registro ? data.registro.split('T')[0] : ''}">
                </div>
            </div>
            <div class="form-group mb-3">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="premium" ${data?.premium ? 'checked' : ''}>
                    <label class="form-check-label">Premium</label>
                </div>
            </div>
        `,
        'profesor': `
            <div class="form-group mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" value="${data?.nombre || ''}" placeholder="Nombre completo">
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Email</label>
                <input type="email" class="form-control" id="email" value="${data?.email || ''}" placeholder="email@example.com">
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Especialidad</label>
                <input type="text" class="form-control" id="especialidad" value="${data?.especialidad || ''}" placeholder="Especialidad">
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Salario</label>
                    <input type="number" class="form-control" id="salario" value="${data?.salario || ''}" step="0.01" placeholder="0.00">
                </div>
                <div class="col-md-6">
                    <label class="form-label">Experiencia (años)</label>
                    <input type="number" class="form-control" id="experiencia" value="${data?.experiencia || '0'}">
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Contrato</label>
                    <input type="date" class="form-control" id="contrato" value="${data?.contrato ? data.contrato.split('T')[0] : ''}">
                </div>
                <div class="col-md-6">
                    <div class="form-check mt-4">
                        <input class="form-check-input" type="checkbox" id="certificado" ${data?.certificado ? 'checked' : ''}>
                        <label class="form-check-label">Certificado</label>
                    </div>
                </div>
            </div>
        `,
        'materia': `
            <div class="form-group mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" value="${data?.nombre || ''}" placeholder="Nombre de la materia">
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Detalle</label>
                <textarea class="form-control" id="detalle" placeholder="Detalles">${data?.detalle || ''}</textarea>
            </div>
            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="form-label">Nivel</label>
                    <input type="number" class="form-control" id="nivel" value="${data?.nivel || '1'}" min="1" max="5">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Cantidad</label>
                    <input type="number" class="form-control" id="cantidad" value="${data?.cantidad || '0'}">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Creación</label>
                    <input type="date" class="form-control" id="creacion" value="${data?.creacion ? data.creacion.split('T')[0] : ''}">
                </div>
            </div>
            <div class="form-group mb-3">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="obligatoria" ${data?.obligatoria ? 'checked' : ''}>
                    <label class="form-check-label">Obligatoria</label>
                </div>
            </div>
        `,
        'inscripcion': `
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Usuario ID</label>
                    <input type="number" class="form-control" id="usuarioId" value="${data?.usuarioId || '1'}">
                </div>
                <div class="col-md-6">
                    <label class="form-label">Curso ID</label>
                    <input type="number" class="form-control" id="cursoId" value="${data?.cursoId || '1'}">
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="form-label">Progreso (%)</label>
                    <input type="number" class="form-control" id="progreso" value="${data?.progreso || '0'}" min="0" max="100">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Nota</label>
                    <input type="number" class="form-control" id="nota" value="${data?.nota || '0'}" step="0.1" min="0" max="10">
                </div>
                <div class="col-md-4">
                    <label class="form-label">Fecha</label>
                    <input type="date" class="form-control" id="inscripcionFecha" value="${data?.inscripcionFecha ? data.inscripcionFecha.split('T')[0] : ''}">
                </div>
            </div>
            <div class="form-group mb-3">
                <label class="form-label">Comentario</label>
                <textarea class="form-control" id="comentario" placeholder="Comentarios">${data?.comentario || ''}</textarea>
            </div>
            <div class="form-group mb-3">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="activa" ${data?.activa ? 'checked' : ''}>
                    <label class="form-check-label">Activa</label>
                </div>
            </div>
        `,
        'leccion': `
            <div class="form-group mb-3">
                <label class="form-label">Título</label>
                <input type="text" class="form-control" id="titulo" value="${data?.titulo || ''}" placeholder="Título de la lección">
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Tipo</label>
                    <select class="form-select" id="tipo">
                        <option value="Video" ${data?.tipo === 'Video' ? 'selected' : ''}>Video</option>
                        <option value="Ejercicio" ${data?.tipo === 'Ejercicio' ? 'selected' : ''}>Ejercicio</option>
                        <option value="Lectura" ${data?.tipo === 'Lectura' ? 'selected' : ''}>Lectura</option>
                        <option value="Evaluación" ${data?.tipo === 'Evaluación' ? 'selected' : ''}>Evaluación</option>
                    </select>
                </div>
                <div class="col-md-6">
                    <label class="form-label">Minutos</label>
                    <input type="number" class="form-control" id="minutos" value="${data?.minutos || '0'}">
                </div>
            </div>
            <div class="form-group mb-3">
                <label class="form-label">URL</label>
                <input type="url" class="form-control" id="url" value="${data?.url || ''}" placeholder="http://ejemplo.com/leccion">
            </div>
            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="form-label">Curso ID</label>
                    <input type="number" class="form-control" id="cursoId" value="${data?.cursoId || '1'}">
                </div>
                <div class="col-md-6">
                    <label class="form-label">Publicación</label>
                    <input type="date" class="form-control" id="publicacion" value="${data?.publicacion ? data.publicacion.split('T')[0] : ''}">
                </div>
            </div>
            <div class="form-group mb-3">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="examen" ${data?.examen ? 'checked' : ''}>
                    <label class="form-check-label">Incluye Examen</label>
                </div>
            </div>
        `
    };
    
    return forms[type] || '<p>Formulario no disponible</p>';
}

function getFormData(type) {
    const getCheckbox = (id) => document.getElementById(id)?.checked || false;
    const getValue = (id) => document.getElementById(id)?.value || '';
    
    const dataMap = {
        'curso': {
            // Usar PascalCase para que coincida con CursoCreateDTO.cs
            Titulo: getValue('titulo'),
            Detalle: getValue('detalle'),
            Costo: parseFloat(getValue('costo')),
            Horas: parseInt(getValue('horas')),
            Publicado: getCheckbox('publicado'),
            Creacion: getValue('creacion'),
            ProfesorId: parseInt(getValue('profesorId')),
            MateriaId: parseInt(getValue('materiaId'))
        },
        'usuario': {
            // Usar PascalCase para que coincida con UsuarioCreateDTO.cs
            Nombre: getValue('nombre'),
            Email: getValue('email'),
            Creditos: parseInt(getValue('creditos')),
            Cursos: parseInt(getValue('cursos')),
            Premium: getCheckbox('premium'),
            Registro: getValue('registro')
        },
        'profesor': {
            // Usar PascalCase para que coincida con ProfesorCreateDTO.cs
            Nombre: getValue('nombre'),
            Especialidad: getValue('especialidad'),
            Salario: parseFloat(getValue('salario')),
            Experiencia: parseInt(getValue('experiencia')),
            Certificado: getCheckbox('certificado'),
            Contrato: getValue('contrato'),
            Email: getValue('email')
        },
        'materia': {
            // Usar PascalCase para que coincida con MateriaCreateDTO.cs
            Nombre: getValue('nombre'),
            Detalle: getValue('detalle'),
            Nivel: parseInt(getValue('nivel')),
            Cantidad: parseInt(getValue('cantidad')),
            Obligatoria: getCheckbox('obligatoria'),
            Creacion: getValue('creacion')
        },
        'inscripcion': {
            // Usar PascalCase para que coincida con InscripcionCreateDTO.cs
            Progreso: getValue('progreso'),
            Comentario: getValue('comentario'),
            Nota: parseFloat(getValue('nota')),
            Activa: getCheckbox('activa'),
            InscripcionFecha: getValue('inscripcionFecha'),
            UsuarioId: parseInt(getValue('usuarioId')),
            CursoId: parseInt(getValue('cursoId'))
        },
        'leccion': {
            // Usar PascalCase para que coincida con LeccionCreateDTO.cs
            Titulo: getValue('titulo'),
            Tipo: getValue('tipo'),
            Minutos: parseInt(getValue('minutos')),
            Examen: getCheckbox('examen'),
            Publicacion: getValue('publicacion'),
            URL: getValue('url'),
            CursoId: parseInt(getValue('cursoId'))
        }
    };
    
    return dataMap[type] || {};
}

// Alerts
function showSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: 'Éxito',
        text: message,
        timer: 2000,
        showConfirmButton: false
    });
}

// Alerts
function showError(title, error) {
    let message = 'Ocurrió un error desconocido.';
    let apiMessage = error.response?.data;
    
    // 1. Manejar Errores de Conexión o Generales
    if (error.message.includes('Network Error')) {
        message = `No se pudo conectar al servidor API (${API_BASE_URL}). Asegúrate de que el backend esté en ejecución y de haber aceptado el certificado HTTPS.`;
    } else if (error.response?.status === 400 && apiMessage?.errors) {
        // 2. Manejar Errores de Validación (Status 400)
        let validationErrors = apiMessage.errors;
        let errorList = '';

        // Formatear los errores de validación de ASP.NET Core
        for (const prop in validationErrors) {
            if (validationErrors.hasOwnProperty(prop)) {
                // Intenta formatear 'Nombre' de la propiedad del DTO
                let propName = prop.replace(/([a-z0-9]|(?=[A-Z]))([A-Z])/g, '$1 $2').trim(); 
                errorList += `<li><strong>${propName}:</strong> ${validationErrors[prop].join(', ')}</li>`;
            }
        }
        
        if (errorList) {
             message = `<p>Se encontraron los siguientes errores de validación:</p><ul>${errorList}</ul>`;
        } else {
             message = apiMessage.message || "Solicitud incorrecta (400 Bad Request).";
        }
        
    } else {
        // 3. Manejar otros errores HTTP (404, 500, etc.)
        // Usar el mensaje general del API si existe
        message = apiMessage?.message || error.message || message;
    }
    
    Swal.fire({
        icon: 'error',
        title: title,
        html: message // Usar 'html' para renderizar la lista <ul>
    });
}