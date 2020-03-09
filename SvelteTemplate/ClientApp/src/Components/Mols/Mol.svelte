<script>
  import Layout from "../../Shared/Layout.svelte";
  import Common from "./Common.svelte";
  import Private from "./Private.svelte";
  import Access from "./Access.svelte";
  import Education from "./Education.svelte";
  import Hist from "./Hist.svelte";
  import page from "page";
  import { route } from "../../store.js";
  import { onMount, onDestroy } from "svelte";

  $: molId = $route.params.id;

  let mol = {};

  let s = route.subscribe(async x => {
    let r = await fetch(`/api/mols/${x.params.id}`);
    mol = await r.json();
  });

  onDestroy(s);


  let comp = Common;


  // onMount(async x => {
  //   let r = await fetch(`/api/mols/${molId}`);
  //   mol = await r.json();
  // });
</script>

<Layout>

  <div class="container">
    <div class="row">
      <div class="col-3">


        <div class="list-group shadow">
          <button
            type="button"
            class="list-group-item list-group-item-action"
            class:active={comp === Common}
            on:click={x => comp = Common}
            >
            Основные данные
          </button>
          <button
            type="button" 
            class="list-group-item list-group-item-action"
            class:active={comp === Private}
            on:click={x => comp = Private}>
            Личные данные
          </button>
          <button type="button" 
          class="list-group-item list-group-item-action"
            class:active={comp === Access}
            on:click={x => comp = Access}
          >
            Доступ КИСП
          </button>
          <button type="button" class="list-group-item list-group-item-action" on:click={x => comp = Education} class:active={comp === Education}>
            Обучение
          </button>
          <button type="button" class="list-group-item list-group-item-action"
           on:click={x => comp = Hist} class:active={comp === Hist}
          >
            История назначений
          </button>
        </div>


        <div class="mt-3">
          <img
            width="100%"
            class="shadow p-3 mb-5 bg-white rounded"
            src="data:image/jpeg;base64, {mol.PHOTO_IMAGE}"
            alt="" />

        </div>

      </div>
      <div class="col-9">
        <div class="card shadow">
          <div class="card-header">
            <div class="card-title">
              <h5>{mol.SURNAME} {mol.NAME1} {mol.NAME2}</h5>
            </div>
          </div>

          <div class="card-body">

            <svelte:component this={comp} {mol}></svelte:component>

            
            

          </div>
        </div>

      </div>
    </div>

  </div>

  <div slot="actions">
    <a class="dropdown-item" href="/" on:click|preventDefault>Действие 1</a>
    <a class="dropdown-item" href="/" on:click|preventDefault>Действие 2</a>
    <div class="dropdown-divider" />
    <a class="dropdown-item" href="/" on:click|preventDefault>Действие 3</a>

  </div>

</Layout>
