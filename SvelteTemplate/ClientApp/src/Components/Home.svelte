<script>
    import Layout from '../Shared/Layout.svelte'
    import {user} from '../Shared/Store'

    async function filterApply() {
        alert()
    }


    

</script>

<Layout {filterApply} >

    <div slot="actions">
              <a class="dropdown-item" href="/">Action</a>
              <a class="dropdown-item" href="/">Another action</a>
              <a class="dropdown-item" href="/">Something else here</a>
    </div>


    <div slot="filter">
        <div class="form-group row">
            <label class="col-form-label col-form-label-sm col-2">asdfasdf</label>
            <div class="col-10">
                <input class="form-control form-control-sm"/>
            </div>
        </div>
    </div>

    <div class="container-fluid">HOME</div>

    {$user.name}


</Layout>
