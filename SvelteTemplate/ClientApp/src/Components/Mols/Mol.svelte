<script context="module">
  import { readable } from "svelte/store";
  export const depts = readable();
</script>

<script>
  import Layout from "../../Layout.svelte";
  import { route } from "../../store.js";
  import { onMount, onDestroy } from "svelte";
  import { asLoader, asRouterLink } from "../../Shared/Actions.js";

  $: molId = $route.params.id;

  let mol = null;
  let depts = [];
  let posts = [];

  let loading = true;

  onMount(async x => {
    loading = true;
    let q = await fetch(`/api/mols/${$route.params.id}`);
    mol = await q.json();

    q = await fetch(`/api/mols/depts`);
    depts = await q.json();

    q = await fetch(`/api/mols/posts`);
    posts = await q.json();
    loading = false;

  });

  let save = async x => {
    loading = true;

    try {
      let q = await fetch(`/api/mols/${x.MOL_ID}`, {
        method: "post",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(x)
      });
      mol = await q.json();
    } catch (err) {
      console.error(err);
    }

    loading = false;
  };


  let comp = null;
  export let components;




</script>

<Layout>



  {#if mol}
    <div class="container">

      <nav class="navbar navbar-expand-lg navbar-light bg-light mb-3 shadow-sm">
        <div class="navbar-brand mr-auto">
          {mol.SURNAME} {mol.NAME1} {mol.NAME2}
        </div>

        <ul class="navbar-nav">
          <li class="nav-item">
            <a href="/mols/{molId}/common" class="nav-link"  use:asRouterLink={'active'}>Основные</a>
          </li>
          <li class="nav-item">
            <a href="/mols/{molId}/private" class="nav-link" use:asRouterLink={'active'}>Личные</a>

          </li>
          <li class="nav-item">
            <a href="/mols/{molId}/photo" class="nav-link" use:asRouterLink={'active'}>Фото</a>
          </li>

          <li class="nav-item" >
            <a href="/mols/{molId}/access" class="nav-item nav-link"use:asRouterLink={'active'}>
              КИСП
              {#if mol.NAME_LOGON}
                <i class="fa fa-check" />
              {:else}
                <i class="fa fa-remove" />
              {/if}
            </a>

          </li>

          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="/"
              role="button"
              data-toggle="dropdown">
              Дополнительно
            </a>
            <div class="dropdown-menu">
              <a class="dropdown-item" href="/mols/{molId}/hist">
                История назначений
              </a>
              <a class="dropdown-item" href="/mols/{molId}/education">
                Обучение
              </a>
            </div>
          </li>
        </ul>

      </nav>

      <div class="row">
        <div class="col-3">

          <div>
              <a href="/mols/{mol.MOL_ID}/photo">
                <img
                  width="100%"
                  class="shadow p-3 mb-5 bg-white rounded"
                  src="{mol.PHOTO_IMAGE? 'data:image/jpeg;base64, ' + mol.PHOTO_IMAGE: '/No-photo-m.png'}"
                  alt="" />
              </a>

          </div>

        </div>
        <div class="col-9">

          <div class="card shadow">
            <div class="card-body">

              <svelte:component
                this={components[0]}
                mol={{ ...mol }}
                {depts}
                {posts}
                {save}
                {loading} />

            </div>
          </div>

        </div>
      </div>

    </div>
  {/if}

</Layout>
