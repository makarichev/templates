<script>
  import Layout from "../../Layout.svelte";
  import Common from "./Common.svelte";
  import page from "page";
  import {asLoader} from "../../Shared/Actions.js";

  let mol = {};
  let loading = false;
  let save = async x => {
      loading = true
      let q = await fetch(`/api/mols/insert`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(x)
      });
      let newMol = await q.json()
      loading = false
      page(`/mols/${newMol.MOL_ID}`);

  };
</script>

<Layout>
  <div class="container">


        <div class="card shadow">
          <div class="card-header">
            <h5>Новый сотрудник</h5>
          </div>
          <div class="card-body">

            <Common {mol} {save} {loading} />

          </div>
        </div>



  </div>
</Layout>

