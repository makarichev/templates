<script>
    import Layout from '../../Shared/Layout.svelte'
    import Edit from './Common.svelte'

    let mol = {}
</script>

<Layout>
  <div class="container">
    <div class="row">

      <div class="col-3">


        <div class="list-group shadow">
          <button
            type="button"
            class="list-group-item list-group-item-action active"
            >
            Основные данные
          </button>
        </div>
      </div>
      <div class="col-9">
        <div class="card shadow">
          <div class="card-header">
            <h5>Новый сотрудник</h5>
          </div>
          <div class="card-body">

            <Edit {mol} on:save={x => alert()}></Edit>

          </div>
        </div>
      </div>

    </div>
  </div>
</Layout>

