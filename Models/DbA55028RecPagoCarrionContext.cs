using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PortalCarrion.Models;

public partial class DbA55028RecPagoCarrionContext : DbContext
{
    public DbA55028RecPagoCarrionContext()
    {
    }

    public DbA55028RecPagoCarrionContext(DbContextOptions<DbA55028RecPagoCarrionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccionesPersonal> AccionesPersonals { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EusExpedienteUsuario> EusExpedienteUsuarios { get; set; }

    public virtual DbSet<InformacionCovid> InformacionCovids { get; set; }

    public virtual DbSet<ReciboPago> ReciboPagos { get; set; }

    public virtual DbSet<UsrUser> UsrUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<AccionesPersonal>(entity =>
        {
            entity.HasKey(e => e.AcpId).HasName("PK__acciones__4C0C38869918798F");

            entity.ToTable("acciones_personal");

            entity.Property(e => e.AcpId).HasColumnName("acp_id");
            entity.Property(e => e.AcpCodexpEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("acp_codexp_empleado");
            entity.Property(e => e.AcpCompaniaEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_compania_empleado");
            entity.Property(e => e.AcpCondicionEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_condicion_empleado");
            entity.Property(e => e.AcpDepartamentoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_departamento_empleado");
            entity.Property(e => e.AcpEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("acp_estado");
            entity.Property(e => e.AcpFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("acp_fecha_creacion");
            entity.Property(e => e.AcpFechaIngresoEmpleado)
                .HasColumnType("datetime")
                .HasColumnName("acp_fecha_ingreso_empleado");
            entity.Property(e => e.AcpFechaRigeHasta)
                .HasColumnType("datetime")
                .HasColumnName("acp_fecha_rige_hasta");
            entity.Property(e => e.AcpFechaRigePartir)
                .HasColumnType("datetime")
                .HasColumnName("acp_fecha_rige_partir");
            entity.Property(e => e.AcpMotivo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("acp_motivo");
            entity.Property(e => e.AcpNombreEmpleado)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acp_nombre_empleado");
            entity.Property(e => e.AcpNuevaCondicion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acp_nueva_condicion");
            entity.Property(e => e.AcpNuevaTienda)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acp_nueva_tienda");
            entity.Property(e => e.AcpNuevoDepartamento)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acp_nuevo_departamento");
            entity.Property(e => e.AcpNuevoPuesto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("acp_nuevo_puesto");
            entity.Property(e => e.AcpNuevoSalario).HasColumnName("acp_nuevo_salario");
            entity.Property(e => e.AcpPuestoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_puesto_empleado");
            entity.Property(e => e.AcpRazon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_razon");
            entity.Property(e => e.AcpSalarioEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_salario_empleado");
            entity.Property(e => e.AcpTiendaEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acp_tienda_empleado");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__empleado__1299A8613BDF3030");

            entity.ToTable("empleados");

            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.EmpCondicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emp_condicion");
            entity.Property(e => e.EmpCodigoAlternativo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emp_codigo_alternativo");
            entity.Property(e => e.EmpCompania)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emp_compania");
            entity.Property(e => e.EmpDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emp_departamento");
            entity.Property(e => e.EmpFechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("emp_fecha_ingreso");
            entity.Property(e => e.EmpNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("emp_nombre");
            entity.Property(e => e.EmpPuesto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emp_puesto");
            entity.Property(e => e.EmpSalario).HasColumnName("emp_salario");
            entity.Property(e => e.EmpTienda)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emp_tienda");
            entity.Property(e => e.EmpCodigoUsuario).HasColumnName("emp_codigo_usuario");
        });

        modelBuilder.Entity<EusExpedienteUsuario>(entity =>
        {
            entity.HasKey(e => e.CodigoEmp).HasName("PK__eus_expe__DC728234D0C455C3");

            entity.ToTable("eus_expediente_usuario");

            entity.Property(e => e.CodigoEmp)
                .ValueGeneratedNever()
                .HasColumnName("Codigo_Emp");
            entity.Property(e => e.EusCodexp).HasColumnName("eus_codexp");
            entity.Property(e => e.EusCodusr).HasColumnName("eus_codusr");

            entity.HasOne(d => d.EusCodusrNavigation).WithMany(p => p.EusExpedienteUsuarios)
                .HasForeignKey(d => d.EusCodusr)
                .HasConstraintName("FK_eus_expediente_usuario_usr_users");
        });

        modelBuilder.Entity<InformacionCovid>(entity =>
        {
            entity.HasKey(e => e.CodEmpleado).HasName("PK__informac__2E2827A000CC6CC1");

            entity.ToTable("informacion_covid");

            entity.Property(e => e.CodEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("cod_empleado");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Embarazo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("embarazo");
            entity.Property(e => e.EnfermedadBase)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("enfermedad_base");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");
            entity.Property(e => e.Identidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identidad");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
        });

        modelBuilder.Entity<ReciboPago>(entity =>
        {
            entity.HasKey(e => e.RpeId).HasName("PK__recibo_p__1FC7FE70C6718D71");

            entity.ToTable("recibo_pago");

            entity.Property(e => e.RpeId).HasColumnName("rpe_id");
            entity.Property(e => e.Afp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("afp");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.MonedaPlanilla)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RpeArea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_area");
            entity.Property(e => e.RpeCentroNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rpe_centro_nombre");
            entity.Property(e => e.RpeCodcia).HasColumnName("rpe_codcia");
            entity.Property(e => e.RpeCodemp).HasColumnName("rpe_codemp");
            entity.Property(e => e.RpeCodexp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_codexp");
            entity.Property(e => e.RpeCodigoDeduccion).HasColumnName("rpe_codigo_deduccion");
            entity.Property(e => e.RpeCodpla)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_codpla");
            entity.Property(e => e.RpeCodpue).HasColumnName("rpe_codpue");
            entity.Property(e => e.RpeCodtipo).HasColumnName("rpe_codtipo");
            entity.Property(e => e.RpeCodtpl).HasColumnName("rpe_codtpl");
            entity.Property(e => e.RpeDeduccion).HasColumnName("rpe_deduccion");
            entity.Property(e => e.RpeDescDeduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_desc_deduccion");
            entity.Property(e => e.RpeDiasDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_dias_desc");
            entity.Property(e => e.RpeDistribucion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_distribucion");
            entity.Property(e => e.RpeDistribucionTotal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_distribucion_total");
            entity.Property(e => e.RpeFechaFin)
                .HasColumnType("datetime")
                .HasColumnName("rpe_fecha_fin");
            entity.Property(e => e.RpeFechaFinLetras)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_fecha_fin_letras");
            entity.Property(e => e.RpeFechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("rpe_Fecha_ingreso");
            entity.Property(e => e.RpeFechaIni)
                .HasColumnType("datetime")
                .HasColumnName("rpe_fecha_ini");
            entity.Property(e => e.RpeFormaPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_forma_pago");
            entity.Property(e => e.RpeIsss)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_isss");
            entity.Property(e => e.RpeMoneda)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_moneda");
            entity.Property(e => e.RpeNit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_nit");
            entity.Property(e => e.RpeNoRecibo).HasColumnName("rpe_no_recibo");
            entity.Property(e => e.RpeNombreEmpleado)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("rpe_nombre_empleado");
            entity.Property(e => e.RpeNombreEmpresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_nombre_empresa");
            entity.Property(e => e.RpeNombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_nombre_tipo");
            entity.Property(e => e.RpeOr).HasColumnName("rpe_or");
            entity.Property(e => e.RpeOrden).HasColumnName("rpe_orden");
            entity.Property(e => e.RpeOrdenTdc).HasColumnName("rpe_orden_tdc");
            entity.Property(e => e.RpePercepcion).HasColumnName("rpe_percepcion");
            entity.Property(e => e.RpePuesto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("rpe_puesto");
            entity.Property(e => e.RpeSalario).HasColumnName("rpe_salario");
            entity.Property(e => e.RpeSalarioHora).HasColumnName("rpe_salario_hora");
            entity.Property(e => e.RpeSaldoPrest).HasColumnName("rpe_saldo_prest");
            entity.Property(e => e.RpeTasa).HasColumnName("rpe_tasa");
            entity.Property(e => e.RpeTiempo).HasColumnName("rpe_tiempo");
            entity.Property(e => e.RpeTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_tipo");
            entity.Property(e => e.RpeUbicacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_ubicacion");
            entity.Property(e => e.RpeUnidad)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("rpe_unidad");
            entity.Property(e => e.RpeUnidadTiempo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_unidad_tiempo");
            entity.Property(e => e.RpeVacacionLeyenda)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_vacacion_leyenda");
            entity.Property(e => e.RpeVacacionLeyenda2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_vacacion_leyenda2");
            entity.Property(e => e.RpeVacacionLeyenda3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rpe_vacacion_leyenda3");
            entity.Property(e => e.RpeValorDeduccion).HasColumnName("rpe_valor_deduccion");
            entity.Property(e => e.TplDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tpl_descripcion");
        });

        modelBuilder.Entity<UsrUser>(entity =>
        {
            entity.HasKey(e => e.UsrCodigo).HasName("PK__usr_user__9AB2F9C0B3A44654");

            entity.ToTable("usr_users");

            entity.Property(e => e.UsrCodigo)
                .ValueGeneratedNever()
                .HasColumnName("usr_codigo");
            entity.Property(e => e.CambioPassPrimerinicio)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("cambio_pass_primerinicio");
            entity.Property(e => e.LeerCondicionesPortal)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("leer_condiciones_portal");
            entity.Property(e => e.LlenarInfoCovid)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("llenar_info_covid");
            entity.Property(e => e.UsrActivo).HasColumnName("usr_activo");
            entity.Property(e => e.UsrCodigoWorkflow)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("usr_codigo_workflow");
            entity.Property(e => e.UsrEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr_email");
            entity.Property(e => e.UsrEstadoWorkflow)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("usr_estado_workflow");
            entity.Property(e => e.UsrFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("usr_fecha_creacion");
            entity.Property(e => e.UsrFechaExpToken)
                .HasColumnType("datetime")
                .HasColumnName("usr_fecha_exp_token");
            entity.Property(e => e.UsrFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("usr_fecha_modificacion");
            entity.Property(e => e.UsrIngresadoPortal).HasColumnName("usr_ingresado_portal");
            entity.Property(e => e.UsrModoAutenticacion)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("usr_modo_autenticacion");
            entity.Property(e => e.UsrNombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usr_nombre_usuario");
            entity.Property(e => e.UsrPassCambiarProxAcceso).HasColumnName("usr_pass_cambiar_prox_acceso");
            entity.Property(e => e.UsrPassUltimoCambio)
                .HasColumnType("datetime")
                .HasColumnName("usr_pass_ultimo_cambio");
            entity.Property(e => e.UsrPassVence).HasColumnName("usr_pass_vence");
            entity.Property(e => e.UsrPassword)
                .HasMaxLength(500)
                .HasColumnName("usr_password");
            entity.Property(e => e.UsrPropertyBagData)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("usr_property_bag_data");
            entity.Property(e => e.UsrTokenResetPwd)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("usr_token_reset_pwd");
            entity.Property(e => e.UsrUltimoAcceso)
                .HasColumnType("datetime")
                .HasColumnName("usr_ultimo_acceso");
            entity.Property(e => e.UsrUsername)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_username");
            entity.Property(e => e.UsrUsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_usuario_creacion");
            entity.Property(e => e.UsrUsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_usuario_modificacion");
            entity.Property(e => e.UsrUsuarioRoot).HasColumnName("usr_usuario_root");
            entity.Property(e => e.UsrVerMismo).HasColumnName("usr_ver_mismo");
            entity.Property(e => e.UsrVerSoloSubaltInmediat).HasColumnName("usr_ver_solo_subalt_inmediat");
            entity.Property(e => e.UsrVerSubalternos).HasColumnName("usr_ver_subalternos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
